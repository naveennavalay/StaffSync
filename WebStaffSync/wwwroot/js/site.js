(function () {
    "use strict";

    const sidebar = document.getElementById("appSidebar");
    const toggle = document.getElementById("sidebarToggle");
    const scrim = document.getElementById("sidebarScrim");

    function closeSidebar() {
        if (!sidebar || !toggle || !scrim) return;
        sidebar.classList.remove("is-open");
        scrim.classList.remove("is-visible");
        toggle.setAttribute("aria-expanded", "false");
    }

    if (sidebar && toggle && scrim) {
        toggle.addEventListener("click", function () {
            const isOpen = sidebar.classList.toggle("is-open");
            scrim.classList.toggle("is-visible", isOpen);
            toggle.setAttribute("aria-expanded", isOpen ? "true" : "false");
        });
        scrim.addEventListener("click", closeSidebar);
        document.addEventListener("keydown", function (event) {
            if (event.key === "Escape") closeSidebar();
        });
        window.addEventListener("resize", function () {
            if (window.innerWidth > 760) closeSidebar();
        });
    }

    const now = new Date();
    const dateOptions = { weekday: "short", day: "2-digit", month: "short", year: "numeric" };
    const formattedDate = now.toLocaleDateString(undefined, dateOptions);
    const topDate = document.getElementById("todayDate");
    const dashboardDate = document.getElementById("dashboardDate");
    if (topDate) topDate.textContent = formattedDate;
    if (dashboardDate) dashboardDate.textContent = now.toLocaleDateString(undefined, {
        weekday: "long", day: "numeric", month: "long", year: "numeric"
    });
})();
