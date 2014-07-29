function displayError() {
    $(function() {
        $("#error").dialog({
            resizable: false,
            height: 250,
            modal: true,
            buttons: {
                "OK": function() {
                    $(this).dialog("close");
                }
            }
        });
    });
};

function displaySuccess() {
    $(function () {
        $("#success").dialog({
            resizable: false,
            height: 250,
            modal: true,
            buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });
    });
};

function displayFailed() {
    $(function () {
        $("#failed").dialog({
            resizable: false,
            height: 250,
            modal: true,
            buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });
    });
};