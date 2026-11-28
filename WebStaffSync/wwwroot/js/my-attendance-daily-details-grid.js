(function (window, $) {
    "use strict";

    var gridElementId = "myAttendanceDailyDetailsGrid";
    var grid = null;
    var requestSequence = 0;

    function valueOf(details, names) {
        for (var i = 0; i < names.length; i++) {
            var key = names[i];
            if (details && details[key] !== undefined && details[key] !== null &&
                String(details[key]).trim() !== "" && String(details[key]) !== "—") {
                return details[key];
            }
        }
        return "—";
    }

    function buildRows(details) {
        details = details || {};

        // Keep the source/API property names explicit. The generic grid converts
        // these two stable keys into its safe internal Kendo field names.
        return [
            { Particulars: "Attendance Status", Details: valueOf(details, ["AttendanceStatus", "AttStatus"]) },
            { Particulars: "Login Time", Details: valueOf(details, ["LoginTime"]) },
            { Particulars: "Late Login Time", Details: valueOf(details, ["LateLoginTime"]) },
            { Particulars: "Late Login Duration", Details: valueOf(details, ["LateLoginDuration"]) },
            { Particulars: "Logout Time", Details: valueOf(details, ["LogoutTime"]) },
            { Particulars: "Late Logout Time", Details: valueOf(details, ["LateLogoutTime"]) },
            { Particulars: "Late Logout Duration", Details: valueOf(details, ["LateLogoutDuration"]) },
            { Particulars: "Total Time", Details: valueOf(details, ["TotalTime"]) },
            { Particulars: "Shift", Details: valueOf(details, ["ShiftTitle"]) },
            { Particulars: "Shift Login", Details: valueOf(details, ["ShiftLoginTime"]) },
            { Particulars: "Shift Logout", Details: valueOf(details, ["ShiftLogoutTime"]) },
            { Particulars: "Grace Period", Details: valueOf(details, ["GracePeriod"]) },
            { Particulars: "Leave Mode", Details: valueOf(details, ["LeaveMode"]) },
            { Particulars: "Leave Duration", Details: valueOf(details, ["LeaveDuration"]) },
            { Particulars: "Leave Comments", Details: valueOf(details, ["LeaveComments"]) }
        ];
    }

    function showMessage(message, isError) {
        var $message = $("#myAttendanceDailyDetailsMessage");
        $message.text(message || "").toggle(!!message);
        $message.toggleClass("text-danger", !!isError);
    }

    function initializeOrRefresh(rows) {
        var element = document.getElementById(gridElementId);
        if (!element) return;

        if (window.StaffSyncKendoGrid &&
            typeof window.StaffSyncKendoGrid.initialize === "function") {

            if (!grid) {
                grid = window.StaffSyncKendoGrid.initialize(element, {
                    data: rows,
                    columns: ["Particulars", "Details"],
                    gridOptions: {
                        columns: [
                            { field: "Particulars", title: "Attribute", width: 220 },
                            { field: "Details", title: "Details", width: 360 }
                        ],
                        sortable: false,
                        filterable: false,
                        groupable: false,
                        pageable: false,
                        reorderable: false,
                        resizable: true,
                        scrollable: false,
                        dataSource: { pageSize: 50 }
                    }
                });
            } else {
                grid.dataSource.data(rows);
                grid.refresh();
            }
            return;
        }

        showMessage("StaffSync generic Kendo Grid script is not loaded. Check script order.", true);
    }

    function load(dateValue, url) {
        if (!dateValue || !url) {
            showMessage("Select a date from the attendance calendar.", false);
            return;
        }

        var sequence = ++requestSequence;
        showMessage("Loading attendance details…", false);

        var requestUrl = url + (url.indexOf("?") >= 0 ? "&" : "?") +
            "date=" + encodeURIComponent(dateValue);

        $.ajax({
            url: requestUrl,
            method: "GET",
            cache: false,
            dataType: "json"
        }).done(function (response) {
            if (sequence !== requestSequence) return;

            if (!response || response.success !== true || !response.details) {
                showMessage((response && response.message) || "No attendance details were returned for this date.", true);
                initializeOrRefresh([]);
                return;
            }

            var rows = buildRows(response.details);
            initializeOrRefresh(rows);
            showMessage("", false);

            var heading = document.getElementById("myAttendanceDailyDetailsDate");
            if (heading) heading.textContent = response.details.Date || dateValue;
        }).fail(function (xhr) {
            if (sequence !== requestSequence) return;
            var message = "Unable to load attendance details.";
            if (xhr.responseJSON && xhr.responseJSON.message) message = xhr.responseJSON.message;
            showMessage(message, true);
            initializeOrRefresh([]);
        });
    }

    window.StaffSyncAttendanceDetailsGrid = {
        load: load,
        refresh: function (rows) { initializeOrRefresh(rows || []); }
    };

})(window, window.jQuery);
