
(function (window, $) {
    "use strict";

    function normalize(value) {
        return String(value == null ? "" : value)
            .toLowerCase()
            .replace(/[^a-z0-9]/g, "");
    }

    function makeSafeFieldName(value) {
        var safe = String(value == null ? "" : value)
            .replace(/[^A-Za-z0-9_$]/g, "");

        if (!safe) safe = "field";
        if (!/^[A-Za-z_$]/.test(safe)) safe = "_" + safe;

        return safe;
    }

    function makeUniqueFieldName(value, usedFields) {
        var base = makeSafeFieldName(value);
        var candidate = base;
        var suffix = 2;

        while (usedFields[candidate]) {
            candidate = base + "_" + suffix++;
        }

        usedFields[candidate] = true;
        return candidate;
    }

    function findMetadata(metadata, field) {
        if (!Array.isArray(metadata)) return null;

        var key = normalize(field);

        return metadata.find(function (m) {
            return m &&
                [m.field, m.sourceName, m.title].some(function (value) {
                    return normalize(value) === key;
                });
        }) || null;
    }

    function parseCustomOptions(json) {
        if (!json) return {};

        try {
            var parsed = typeof json === "string"
                ? JSON.parse(json)
                : json;

            return parsed &&
                typeof parsed === "object" &&
                !Array.isArray(parsed)
                ? parsed
                : {};

        } catch (error) {
            console.warn(
                "StaffSync grid: invalid KendoOptionsJson ignored.",
                error
            );
            return {};
        }
    }

    function normalizeDataType(value) {
        var type = String(value || "string").toLowerCase();

        if (["date", "datetime", "time"].indexOf(type) >= 0)
            return type;

        if (["number", "numeric", "currency", "amount"].indexOf(type) >= 0)
            return "number";

        if (["boolean", "bool"].indexOf(type) >= 0)
            return "boolean";

        return "string";
    }

    function parseDateValue(value, meta) {
        if (value === null || value === undefined || value === "")
            return null;

        if (value instanceof Date)
            return isNaN(value.getTime()) ? null : value;

        if (typeof value === "number") {
            var numericDate = new Date(value);
            return isNaN(numericDate.getTime()) ? null : numericDate;
        }

        var formats = [];

        if (meta && meta.format)
            formats.push(meta.format);

        formats = formats.concat([
            "dd-MMM-yyyy hh:mm:ss tt",
            "dd-MMM-yyyy h:mm:ss tt",
            "dd-MMM-yyyy",
            "yyyy-MM-ddTHH:mm:ss.fffZ",
            "yyyy-MM-ddTHH:mm:ssZ",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-dd",
            "MM/dd/yyyy",
            "M/d/yyyy",
            "HH:mm:ss",
            "hh:mm:ss tt"
        ]);

        if (window.kendo &&
            typeof window.kendo.parseDate === "function") {

            var parsed = window.kendo.parseDate(
                String(value),
                formats
            );

            if (parsed && !isNaN(parsed.getTime()))
                return parsed;
        }

        var fallback = new Date(value);

        return isNaN(fallback.getTime()) ? null : fallback;
    }

    function parseBooleanValue(value) {
        if (value === true || value === false)
            return value;

        if (typeof value === "number")
            return value !== 0;

        if (typeof value === "string") {
            var normalized = value.trim().toLowerCase();

            if (["true", "1", "yes", "y"].indexOf(normalized) >= 0)
                return true;

            if (["false", "0", "no", "n", ""].indexOf(normalized) >= 0)
                return false;
        }

        return value;
    }

    function makeColumn(meta, field) {
        var alignment =
            ["left", "center", "right"].indexOf(meta.alignment) >= 0
                ? meta.alignment
                : "left";

        var column = {
            field: field || makeSafeFieldName(
                meta.field || meta.sourceName || meta.title
            ),
            title: meta.title || meta.sourceName || meta.field,
            hidden: meta.visible === false,
            width: Number(meta.width) > 0
                ? Number(meta.width)
                : 150,
            sortable: meta.sortable !== false,
            filterable: meta.filterable !== false,
            groupable: meta.groupable === true,
            resizable: meta.resizable !== false,
            reorderable: meta.reorderable !== false,
            locked: meta.locked === true,
            lockable: meta.lockable !== false,
            attributes: {
                style: "text-align:" + alignment + ";"
            },
            headerAttributes: {
                style: "text-align:" + alignment + ";"
            }
        };

        var dataType = normalizeDataType(meta.dataType);
        var trueText = meta.trueText || "Yes";
        var falseText = meta.falseText || "No";

        if (meta.format) {
            column.format = "{0:" + meta.format + "}";
        }

        // Configurable Boolean display labels.
        if (dataType === "boolean") {
            var booleanField = column.field;

            column.template = function (item) {
                var value = item ? item[booleanField] : null;

                value = parseBooleanValue(value);

                if (value === true) return trueText;
                if (value === false) return falseText;

                return value == null ? "" : String(value);
            };
        }

        // Preserve application-specific Kendo options.
        var customOptions = parseCustomOptions(
            meta.kendoOptionsJson
        );

        Object.keys(customOptions).forEach(function (key) {
            if (
                ["field", "hidden", "title", "width"]
                    .indexOf(key) < 0
            ) {
                column[key] = customOptions[key];
            }
        });

        return column;
    }

    function resolveMappedField(metadata, fieldMap, meta) {
        if (!meta) return null;

        return fieldMap[meta.sourceName] ||
            fieldMap[meta.field] ||
            fieldMap[meta.title] ||
            makeSafeFieldName(
                meta.field || meta.sourceName || meta.title
            );
    }

    function buildSchema(metadata, fields, fieldMap) {
        var schemaFields = {};

        (metadata || []).forEach(function (meta) {
            if (!meta) return;

            var mappedField = resolveMappedField(
                metadata,
                fieldMap || {},
                meta
            );

            if (!mappedField) return;

            var dataType = normalizeDataType(meta.dataType);

            var schemaField = {
                type: dataType === "datetime" || dataType === "time"
                    ? "date"
                    : dataType
            };

            if (
                dataType === "date" ||
                dataType === "datetime" ||
                dataType === "time"
            ) {
                schemaField.parse = function (value) {
                    return parseDateValue(value, meta);
                };

            } else if (dataType === "boolean") {
                schemaField.parse = parseBooleanValue;
            }

            schemaFields[mappedField] = schemaField;
        });

        (fields || []).forEach(function (field) {
            if (!schemaFields[field]) {
                schemaFields[field] = {
                    type: "string"
                };
            }
        });

        return schemaFields;
    }

    function buildSort(metadata, fieldMap) {
        return (metadata || [])
            .filter(function (meta) {
                return meta &&
                    Number(meta.sortOrder) >= 0 &&
                    (
                        meta.sortDirection === "asc" ||
                        meta.sortDirection === "desc"
                    );
            })
            .sort(function (a, b) {
                return Number(a.sortOrder) -
                    Number(b.sortOrder);
            })
            .map(function (meta) {
                return {
                    field: resolveMappedField(
                        metadata,
                        fieldMap,
                        meta
                    ),
                    dir: meta.sortDirection
                };
            })
            .filter(function (item) {
                return !!item.field;
            });
    }

    function buildGroups(metadata, fieldMap) {
        return (metadata || [])
            .filter(function (meta) {
                return meta &&
                    meta.groupable === true &&
                    Number(meta.groupOrder) >= 0;
            })
            .sort(function (a, b) {
                return Number(a.groupOrder) -
                    Number(b.groupOrder);
            })
            .map(function (meta) {
                return {
                    field: resolveMappedField(
                        metadata,
                        fieldMap,
                        meta
                    )
                };
            })
            .filter(function (item) {
                return !!item.field;
            });
    }

    function applyMetadata(columns, metadata, fieldMap) {
        return columns.map(function (column) {
            var meta = findMetadata(
                metadata,
                column.sourceName ||
                column.field ||
                column.title
            );

            if (!meta) {
                var safeColumn = $.extend(
                    true,
                    {},
                    column
                );

                safeColumn.field = makeSafeFieldName(
                    column.field || column.title
                );

                return safeColumn;
            }

            var mappedField = resolveMappedField(
                metadata,
                fieldMap || {},
                meta
            );

            var configured = makeColumn(
                meta,
                mappedField
            );

            // Preserve custom templates, commands and settings.
            Object.keys(column).forEach(function (key) {
                if (configured[key] === undefined) {
                    configured[key] = column[key];
                }
            });

            return configured;
        });
    }

    function getActualRowKey(row, source, target) {
        if (!row || typeof row !== "object")
            return undefined;

        var keys = Object.keys(row);
        var normalizedSource = normalize(source);
        var normalizedTarget = normalize(target);

        return keys.find(function (key) {
            return normalize(key) === normalizedSource;
        }) || keys.find(function (key) {
            return normalize(key) === normalizedTarget;
        });
    }

    function initialize(element, options) {
        // The helper may be loaded before jQuery/Kendo UI by a layout bundle.
        // Resolve the current global jQuery at call time instead of relying
        // only on the value captured when this file was first evaluated.
        if (typeof $ === "undefined" || !$ || !$.fn) {
            $ = window.jQuery || window.$;
        }

        if (
            !element ||
            !$ ||
            !$.fn ||
            typeof $.fn.kendoGrid !== "function"
        ) {
            console.error(
                "StaffSync grid: jQuery/Kendo UI is not ready. Load jQuery first, then Kendo UI, then initialize the grid."
            );
            return null;
        }

        options = options || {};

        var $element = $(element);
        var existingGrid = $element.data("kendoGrid");

        if (existingGrid)
            return existingGrid;

        var payload = options.payload || {};

        var rows = Array.isArray(options.data)
            ? options.data
            : (
                Array.isArray(payload.data)
                    ? payload.data
                    : []
            );

        var metadata = Array.isArray(options.columnMetadata)
            ? options.columnMetadata
            : (
                Array.isArray(payload.columnMetadata)
                    ? payload.columnMetadata
                    : []
            );

        var sourceNames = Array.isArray(options.columns)
            ? options.columns
            : (
                Array.isArray(payload.columns)
                    ? payload.columns
                    : (
                        rows.length
                            ? Object.keys(rows[0])
                            : []
                    )
            );

        // Preserve configured column ordering.
        sourceNames = sourceNames.map(function (source, index) {
            var meta = findMetadata(metadata, source);

            var order = meta &&
                meta.order !== undefined &&
                meta.order !== null
                ? Number(meta.order)
                : -1;

            return {
                source: source,
                originalIndex: index,
                order:
                    Number.isFinite(order) && order >= 0
                        ? order
                        : Number.MAX_SAFE_INTEGER
            };

        }).sort(function (a, b) {
            return a.order - b.order ||
                a.originalIndex - b.originalIndex;

        }).map(function (item) {
            return item.source;
        });

        var fieldMap = Object.create(null);
        var usedFields = Object.create(null);

        var columns = sourceNames.map(function (source) {
            var meta = findMetadata(metadata, source);

            var requestedField = meta && meta.field
                ? meta.field
                : source;

            var safeField = makeUniqueFieldName(
                requestedField,
                usedFields
            );

            fieldMap[source] = safeField;

            if (meta) {
                if (meta.field)
                    fieldMap[meta.field] = safeField;

                if (meta.sourceName)
                    fieldMap[meta.sourceName] = safeField;

                if (meta.title)
                    fieldMap[meta.title] = safeField;
            }

            var column = meta
                ? makeColumn(meta, safeField)
                : {
                    field: safeField,
                    title: source
                };

            column.sourceName = source;

            return column;
        });

        // Map original data keys to safe internal Kendo fields.
        var mappedRows = rows.map(function (row) {
            var mappedRow = {};

            sourceNames.forEach(function (source) {
                var target = fieldMap[source] ||
                    makeSafeFieldName(source);

                var actualKey = getActualRowKey(
                    row,
                    source,
                    target
                );

                mappedRow[target] =
                    actualKey === undefined
                        ? null
                        : row[actualKey];
            });

            return mappedRow;
        });

        var defaultGridOptions = {
            dataSource: {
                data: mappedRows,

                schema: {
                    model: {
                        fields: buildSchema(
                            metadata,
                            columns.map(function (column) {
                                return column.field;
                            }),
                            fieldMap
                        )
                    }
                },

                sort: buildSort(metadata, fieldMap),
                group: buildGroups(metadata, fieldMap),
                pageSize: 50
            },

            columns: columns,

            sortable: true,
            filterable: true,
            groupable: false,

            pageable: {
                refresh: true,
                pageSizes: [10, 20, 50, 100, 250, 500],
                buttonCount: 5
            },

            resizable: true,
            reorderable: true,
            scrollable: true
        };

        var gridOptions = $.extend(
            true,
            {},
            defaultGridOptions,
            options.gridOptions || {}
        );

        // Apply metadata to caller-supplied columns too.
        if (
            options.gridOptions &&
            Array.isArray(options.gridOptions.columns)
        ) {
            gridOptions.columns = applyMetadata(
                options.gridOptions.columns,
                metadata,
                fieldMap
            );
        }

        return $element.kendoGrid(
            gridOptions
        ).data("kendoGrid");
    }

    var api = {
        version: "1.2.0",

        findColumnMetadata: findMetadata,

        buildColumns: function (metadata) {
            return (metadata || [])
                .filter(Boolean)
                .map(function (meta) {
                    return makeColumn(
                        meta,
                        makeSafeFieldName(
                            meta.field ||
                            meta.sourceName ||
                            meta.title
                        )
                    );
                });
        },

        applyColumnOptions: function (column, metadata) {
            if (!column || !metadata)
                return column;

            var safeField = makeSafeFieldName(
                column.field ||
                metadata.field ||
                metadata.sourceName ||
                metadata.title
            );

            return $.extend(
                true,
                column,
                makeColumn(metadata, safeField)
            );
        },

        applyGridMetadata: function (columns, metadata) {
            return applyMetadata(
                columns,
                metadata,
                {}
            );
        },

        initialize: initialize,

        initializeAll: function (selector, defaults) {
            selector = selector || "[data-staffsync-grid]";

            var grids = [];

            $(selector).each(function (index) {
                var element = this;
                var payload;

                try {
                    payload = JSON.parse(
                        element.getAttribute("data-grid") || "{}"
                    );

                } catch (error) {
                    console.error(
                        "StaffSync grid: invalid data-grid JSON for grid " +
                        (index + 1) + ".",
                        element,
                        error
                    );

                    grids.push(null);
                    return;
                }

                var perGrid = $.extend(
                    true,
                    {},
                    defaults || {},
                    {
                        payload: payload,
                        gridOptions: payload.gridOptions || {}
                    }
                );

                // Isolate failures so one grid cannot block others.
                try {
                    grids.push(
                        initialize(element, perGrid)
                    );

                } catch (error) {
                    console.error(
                        "StaffSync grid: initialization failed for grid " +
                        (index + 1) + ".",
                        element,
                        error
                    );

                    grids.push(null);
                }
            });

            return grids;
        }
    };

    window.StaffSyncKendoGrid = api;

})(window, window.jQuery);
