const registry = new Map();
function onDocClick(e) {
    registry.forEach((ref, elem) => {
        if (!elem.contains(e.target)) {
            ref.invokeMethodAsync("Close");
        }
    });
}

export function register(elem, dotNetRef) {
    if (registry.size === 0) {
        document.addEventListener("click", onDocClick, true);
    }
    registry.set(elem, dotNetRef);
}

export function unregister(elem) {
    registry.delete(elem);
    if (registry.size === 0) {
        document.removeEventListener("click", onDocClick, true);
    }
}

window.focusHelper = {
    next: function (elem) {
        const tabbables = Array.from(document.querySelectorAll('[tabindex]:not([tabindex="-1"])'))
            .filter(el => !el.disabled && el.offsetParent !== null);
        const idx = tabbables.indexOf(elem);
        if (idx > -1 && idx < tabbables.length - 1) {
            tabbables[idx + 1].focus();
        }
    },
    prev: function (elem) {
        const tabbables = Array.from(document.querySelectorAll('[tabindex]:not([tabindex="-1"])'))
            .filter(el => !el.disabled && el.offsetParent !== null);
        const idx = tabbables.indexOf(elem);
        if (idx > 0) {
            tabbables[idx - 1].focus();
        }
    }
};