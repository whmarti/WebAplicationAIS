var g_XMLHttpRequest;
function CallService(request) {

    $.ajax({
        type: request.type,
        url: request.url,
        data: request.params,
        contentType: request.contentType,
        dataType: request.dataType,
        cache: false,
        beforeSend: function () {

        },
        success: function (data) {


            g_XMLHttpRequest = {
                resultado: data
            };

            eval(request.success + "();");

        },
        error: function (xhr, ajaxOptions, thrownError) {

            g_XMLHttpRequest = {

                xhr: xhr,
                ajaxOptions: ajaxOptions,
                thrownError: thrownError

            };

            eval(request.error + "();");
        },
        statusCode: {
            404: function () {
                alertify.alert("Page not FOund.");
            }
        }
    });
}

function ServiceFailed() {

    if (g_XMLHttpRequest.xhr.responseText !== "") {
        var err = g_XMLHttpRequest.xhr.responseText;
        console.log(err);

    }
    else if (g_XMLHttpRequest.ajaxOptions !== "") {
        var error = g_XMLHttpRequest.thrownError;
        console.log(error);
    }
    else
        console.log("Error desconocido de Servidor.");
    //$(".spinner").css("visibility", "hidden");
    $('#cover-spin').hide();
    return;
}