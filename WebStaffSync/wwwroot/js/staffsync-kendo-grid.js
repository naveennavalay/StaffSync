(function (window, $) {
    "use strict";

    // Normalize CLR property names, humanized captions and display titles so
    // metadata still matches when the API/UI uses different spacing or punctuation.
    function normalize(value) {
        return String(value == null ? "" : value).toLowerCase().replace(/[^a-z0-9]/g, "");
    }

    function findColumnMetadata(metadata, field) {
        if (!Array.isArray(metadata) || !field) return null;
        var key = normalize(field);
        for (var i = 0; i < metadata.length; i++) {
            var item = metadata[i];
            if (!item) continue;
            if ([item.field, item.sourceName, item.title].some(function (candidate) {
                return normalize(candidate) === key;
            })) return item;
        }
        return null;
    }

    function mergeStyle(existing, addition) {
        var current = String(existing || "").replace(/text-align\s*:[^;]+;?/ig, "");
        return (current ? current.replace(/;?$/, ";") : "") + addition;
    }

    var api = {
        findColumnMetadata: findColumnMetadata,

        applyColumnOptions: function (kendoColumn, metadata) {
            if (!kendoColumn || !metadata) return kendoColumn;
            if (metadata.title) kendoColumn.title = metadata.title;

            // Kendo Grid uses `hidden`, not `visible`, in its column definition.
            if (typeof metadata.visible === "boolean") kendoColumn.hidden = !metadata.visible;
            else if (typeof metadata.hidden === "boolean") kendoColumn.hidden = metadata.hidden;

            var width = parseInt(metadata.width, 10);
            if (!isNaN(width) && width > 0) kendoColumn.width = width;

            var alignment = String(metadata.alignment || "left").toLowerCase();
            if (["left", "center", "right"].indexOf(alignment) < 0) alignment = "left";
            kendoColumn.attributes = kendoColumn.attributes || {};
            kendoColumn.headerAttributes = kendoColumn.headerAttributes || {};
            kendoColumn.attributes.style = mergeStyle(kendoColumn.attributes.style, "text-align:" + alignment + ";");
            kendoColumn.headerAttributes.style = mergeStyle(kendoColumn.headerAttributes.style, "text-align:" + alignment + ";");

            if (metadata.format) kendoColumn.format = "{0:" + String(metadata.format) + "}";
            return kendoColumn;
        },

        applyGridMetadata: function (columns, metadata) {
            if (!Array.isArray(columns)) return columns || [];
            return columns.map(function (column) {
                var match = findColumnMetadata(metadata, column.sourceName || column.field || column.title);
                if (match) api.applyColumnOptions(column, match);
                return column;
            });
        },

        // Apply explicit visibility to a live grid as a final safeguard. This is
        // useful for grids whose column definitions are assembled dynamically.
        applyVisibilityToGrid: function (grid, columns, metadata) {
            if (!grid || !Array.isArray(columns) || !Array.isArray(metadata)) return;
            columns.forEach(function (column, index) {
                var match = findColumnMetadata(metadata, column.sourceName || column.field || column.title);
                if (!match || typeof match.visible !== "boolean") return;
                var liveColumn = grid.columns[index];
                if (!liveColumn) return;
                if (match.visible === false && !liveColumn.hidden) grid.hideColumn(liveColumn);
                else if (match.visible === true && liveColumn.hidden) grid.showColumn(liveColumn);
            });
        }
    };

    window.StaffSyncKendoGrid = api;
})(window, window.jQuery);
