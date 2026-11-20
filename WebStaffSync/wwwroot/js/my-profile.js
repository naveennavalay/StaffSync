(function () {
    "use strict";
    const tabs = Array.from(document.querySelectorAll("[data-profile-tab]"));
    const panels = Array.from(document.querySelectorAll(".profile-panel"));
    if (!tabs.length || !panels.length) return;

    function activate(tab) {
        const key = tab.getAttribute("data-profile-tab");
        tabs.forEach(item => {
            const active = item === tab;
            item.classList.toggle("is-active", active);
            item.setAttribute("aria-selected", active ? "true" : "false");
            item.tabIndex = active ? 0 : -1;
        });
        panels.forEach(panel => {
            const active = panel.id === "panel-" + key;
            panel.classList.toggle("is-active", active);
            panel.hidden = !active;
        });
    }

    tabs.forEach((tab, index) => {
        tab.addEventListener("click", () => activate(tab));
        tab.addEventListener("keydown", event => {
            if (event.key !== "ArrowRight" && event.key !== "ArrowLeft") return;
            event.preventDefault();
            const direction = event.key === "ArrowRight" ? 1 : -1;
            const next = tabs[(index + direction + tabs.length) % tabs.length];
            activate(next);
            next.focus();
        });
    });
})();
