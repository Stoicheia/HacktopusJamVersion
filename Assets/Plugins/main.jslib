mergeInto(LibraryManager.library, {

    DispatchEvent: function (str, score) {
        const event = new CustomEvent(str, { detail: score });
        addEventListener(
          str, (e) => {window.alert(score);}, false
        );
        dispatchEvent(event);
    }

});