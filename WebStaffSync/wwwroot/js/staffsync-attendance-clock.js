(() => {
    "use strict";

    const root = document.getElementById("staffSyncAttendanceClock");
    if (!root) return;

    const $ = id => document.getElementById(id);
    const urls = {
        state: root.dataset.stateUrl,
        login: root.dataset.loginUrl,
        logout: root.dataset.logoutUrl
    };

    const token = root.querySelector('input[name="__RequestVerificationToken"]')?.value || "";
    let state = null;
    let serverOffsetMs = 0;
    let refreshHandle = null;
    let busy = false;

    const read = (obj, ...names) => {
        for (const name of names) {
            if (obj && obj[name] !== undefined && obj[name] !== null) return obj[name];
        }
        return null;
    };

    function parseDate(value) {
        if (!value) return null;
        const d = new Date(value);
        return Number.isNaN(d.getTime()) ? null : d;
    }

    function parseClock(value) {
        if (!value) return null;
        if (value instanceof Date) return value;
        const text = String(value);
        const match = text.match(/(\d{1,2}):(\d{2})(?::(\d{2}))?\s*(AM|PM)?/i);
        if (!match) return parseDate(value);
        let h = Number(match[1]);
        const m = Number(match[2]);
        const s = Number(match[3] || 0);
        const ap = (match[4] || "").toUpperCase();
        if (ap === "PM" && h < 12) h += 12;
        if (ap === "AM" && h === 12) h = 0;
        const d = new Date();
        d.setHours(h, m, s, 0);
        return d;
    }

    function fmtTime(value) {
        const d = parseDate(value) || parseClock(value);
        return d ? d.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit", hour12: true }) : "Pending";
    }

    function durationText(ms) {
        const total = Math.max(0, Math.floor(ms / 1000));
        const h = Math.floor(total / 3600);
        const m = Math.floor((total % 3600) / 60);
        const s = total % 60;
        return [h, m, s].map(v => String(v).padStart(2, "0")).join(":");
    }

    function setMessage(message, kind = "") {
        const el = $("staffSyncAttendanceMessage");
        el.textContent = message || "";
        el.className = "staffsync-attendance-message" + (kind ? " " + kind : "");
    }

    function setStatus(status) {
        const el = $("staffSyncAttendanceStatus");
        const label = el.querySelector("span:last-child");
        const loggedIn = status === "LoggedIn";
        const loggedOut = status === "LoggedOut";
        label.textContent = loggedIn ? "Logged In" : (loggedOut ? "Logged Out" : "Not Logged In");
        el.classList.toggle("is-logged-in", loggedIn);
        el.classList.toggle("is-logged-out", !loggedIn);
    }

    function setTiming(id, text, good) {
        const el = $(id);
        el.textContent = text;
        el.classList.toggle("is-good", !!good);
        el.classList.toggle("is-warning", !good && text !== "--");
    }

    function shiftWindow() {
        if (!state) return null;
        const startClock = parseClock(read(state, "shiftLoginTime", "ShiftLoginTime"));
        const endClock = parseClock(read(state, "shiftLogoutTime", "ShiftLogoutTime"));
        if (!startClock || !endClock) return null;

        const now = new Date(Date.now() + serverOffsetMs);
        const start = new Date(now);
        start.setHours(startClock.getHours(), startClock.getMinutes(), startClock.getSeconds(), 0);
        const end = new Date(now);
        end.setHours(endClock.getHours(), endClock.getMinutes(), endClock.getSeconds(), 0);
        // Support shifts that cross midnight.
        if (end <= start) end.setDate(end.getDate() + 1);
        // If the shift began yesterday and has not ended yet, anchor to yesterday.
        if (now < start && (start - now) > 12 * 60 * 60 * 1000) {
            start.setDate(start.getDate() - 1);
            end.setDate(end.getDate() - 1);
        }
        return { start, end, now };
    }

    function renderTimeline() {
        const window = shiftWindow();
        if (!window) {
            $("staffSyncShiftHourMarkers").innerHTML = "";
            return;
        }

        const { start, end, now } = window;
        const total = end - start;
        const elapsed = Math.min(total, Math.max(0, now - start));
        const percent = total > 0 ? Math.max(0, Math.min(100, elapsed / total * 100)) : 0;
        const remaining = Math.max(0, total - elapsed);

        $("staffSyncShiftProgressFill").style.width = percent.toFixed(2) + "%";
        $("staffSyncShiftProgressFill").parentElement.setAttribute("aria-valuenow", percent.toFixed(0));
        $("staffSyncShiftElapsed").textContent = durationText(elapsed);
        $("staffSyncShiftRemaining").textContent = durationText(remaining);
        $("staffSyncShiftElapsedPercent").textContent = Math.floor(percent) + "% elapsed";
        $("staffSyncShiftRemainingPercent").textContent = Math.ceil(100 - percent) + "% remaining";

        const markers = $("staffSyncShiftHourMarkers");
        const points = [];
        const first = new Date(start);
        first.setMinutes(0, 0, 0);
        if (first < start) first.setHours(first.getHours() + 1);

        const addMarker = (date, label, boundary = false) => {
            const position = Math.max(0, Math.min(100, (date - start) / total * 100));
            points.push(`<span class="staffsync-shift-hour-marker ${boundary ? "is-boundary" : ""}" style="left:${position}%"><i></i><small>${label}</small></span>`);
        };

        addMarker(start, fmtTime(start), true);
        for (let cursor = new Date(first); cursor < end; cursor.setHours(cursor.getHours() + 1)) {
            addMarker(new Date(cursor), fmtTime(cursor));
        }
        addMarker(end, fmtTime(end), true);
        markers.innerHTML = points.join("");
    }

    function render() {
        if (!state) return;

        const status = read(state, "status", "Status") || "NotLoggedIn";
        const shiftTitle = read(state, "shiftTitle", "ShiftTitle") || "Assigned Shift";
        const login = read(state, "loginTime", "LoginTime");
        const logout = read(state, "logoutTime", "LogoutTime");
        const total = read(state, "totalTime", "TotalTime");

        $("staffSyncShiftTitle").textContent = shiftTitle;
        $("staffSyncShiftLogin").textContent = fmtTime(read(state, "shiftLoginTime", "ShiftLoginTime"));
        $("staffSyncShiftLogout").textContent = fmtTime(read(state, "shiftLogoutTime", "ShiftLogoutTime"));
        $("staffSyncActualLogin").textContent = fmtTime(login);
        $("staffSyncActualLogout").textContent = fmtTime(logout);

        const loginDate = parseDate(login);
        const logoutDate = parseDate(logout);
        let diff = "--:--:--";
        if (loginDate && logoutDate) diff = durationText(logoutDate - loginDate);
        else if (loginDate && !logoutDate) diff = durationText(Date.now() + serverOffsetMs - loginDate.getTime());
        else if (total) {
            const totalDate = parseDate(total);
            if (totalDate) diff = durationText(
                ((totalDate.getHours() * 3600 + totalDate.getMinutes() * 60 + totalDate.getSeconds()) * 1000));
        }
        $("staffSyncTimeDifference").textContent = diff;

        const shiftStart = parseClock(read(state, "shiftLoginTime", "ShiftLoginTime"));
        const shiftEnd = parseClock(read(state, "shiftLogoutTime", "ShiftLogoutTime"));
        if (loginDate && shiftStart) {
            const actualMins = loginDate.getHours() * 60 + loginDate.getMinutes();
            let shiftMins = shiftStart.getHours() * 60 + shiftStart.getMinutes();
            setTiming("staffSyncLoginTimingStatus",
                actualMins > shiftMins + getGraceMinutes() ? "Late Login" : "On Time Login",
                actualMins <= shiftMins + getGraceMinutes());
        } else setTiming("staffSyncLoginTimingStatus", "--", false);

        if (logoutDate && shiftEnd) {
            const actualMins = logoutDate.getHours() * 60 + logoutDate.getMinutes();
            let shiftMins = shiftEnd.getHours() * 60 + shiftEnd.getMinutes();
            setTiming("staffSyncLogoutTimingStatus",
                actualMins < shiftMins - getGraceMinutes() ? "Early Logout" : "On Time Logout",
                actualMins >= shiftMins - getGraceMinutes());
        } else setTiming("staffSyncLogoutTimingStatus", "--", false);

        setStatus(status);
        const loginButton = $("staffSyncAttendanceLogin");
        const logoutButton = $("staffSyncAttendanceLogout");
        loginButton.disabled = busy || !!login || !!logout || !read(state, "attID", "AttID");
        logoutButton.disabled = busy || !login || !!logout;

        renderTimeline();
    }

    function getGraceMinutes() {
        const value = read(state, "gracePeriod", "GracePeriod");
        const d = parseClock(value);
        return d ? d.getHours() * 60 + d.getMinutes() : 0;
    }

    async function request(url, method = "GET") {
        const headers = { "Accept": "application/json" };
        if (method !== "GET") {
            headers["Content-Type"] = "application/json";
            headers["RequestVerificationToken"] = token;
        }
        const response = await fetch(url, {
            method,
            headers,
            credentials: "same-origin",
            cache: "no-store",
            body: method === "GET" ? undefined : "{}"
        });
        const body = await response.json().catch(() => ({}));
        if (!response.ok) {
            const error = new Error(body.message || `Attendance request failed (HTTP ${response.status}).`);
            error.status = response.status;
            throw error;
        }
        return body;
    }

    async function refresh(showLoading = false) {
        if (showLoading) setMessage("Checking today's attendance...");
        try {
            const before = Date.now();
            const data = await request(urls.state);
            const after = Date.now();
            // API returns server-generated AttDate; estimate offset using HTTP Date is not
            // exposed through this same-origin JSON proxy. Browser time is display-only.
            state = data;
            serverOffsetMs = 0;
            render();
            if (showLoading) setMessage(read(data, "message", "Message") || "Attendance information loaded.", "is-success");
        } catch (error) {
            setMessage(error.message, "is-error");
            $("staffSyncAttendanceLogin").disabled = true;
            $("staffSyncAttendanceLogout").disabled = true;
        }
    }

    async function perform(action) {
        if (busy) return;
        const isLogout = action === "logout";
        if (isLogout && !window.confirm("Are you sure you want to record today's Logout?")) return;

        busy = true;
        render();
        setMessage(isLogout ? "Recording Logout..." : "Recording Login...");
        try {
            state = await request(isLogout ? urls.logout : urls.login, "POST");
            render();
            setMessage(read(state, "message", "Message") || "Attendance updated successfully.", "is-success");
        } catch (error) {
            setMessage(error.message, "is-error");
            await refresh(false);
        } finally {
            busy = false;
            render();
        }
    }

    $("staffSyncAttendanceLogin").addEventListener("click", () => perform("login"));
    $("staffSyncAttendanceLogout").addEventListener("click", () => perform("logout"));

    refresh(true);
    refreshHandle = window.setInterval(() => {
        if (state) renderTimeline();
        refresh(false);
    }, 60000);

    window.addEventListener("beforeunload", () => {
        if (refreshHandle) window.clearInterval(refreshHandle);
    });
})();
