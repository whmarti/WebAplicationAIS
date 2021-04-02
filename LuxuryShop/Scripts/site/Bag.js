function addItem(pIdProd) {
    

    _type = "POST";
    _contentType = "application/json; charset=utf-8"; 
    _url = 'Bag.aspx/WebM_AddCarItem';
    let obj = {};
    obj.pIdProd = $.trim(pIdProd);

    var request = {
        type: _type,
        contentType: _contentType,
        url: _url,
        params: JSON.stringify(obj),
        success: 'addItem_final',
        error: 'ServiceFailed',
        dataType: "json"
    };
    CallService(request);
}

//Function with te result of the operation.
function addItem_final() {
    var _obj = g_XMLHttpRequest.resultado.d;
    let _searchParams = new URLSearchParams(window.location.search)    
       
    if (_obj.success == true) {
        //alert(_obj.msg);
        showMsg(_obj.success, _obj.msg);
    }
    else if ( _obj.msg.indexOf("1") >= 0) {       
       //alert(_obj.msg);
        showMsg(_obj.success, _obj.msg);
    }
    else {
        let _prod = "";
        $.urlParam = function (name) {
            var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
            if (results == null) {
                return null;
            }
            return decodeURI(results[1]) || 0;
        }
        _prod = $.urlParam('s');
        if ((_obj.msg.indexOf("0") >= 0) && (!_obj.success)) {
            document.location.href = "Login.aspx?s=" + _prod;
        }
    }
    //$(".spinner").css("visibility", "hidden");
    $('#cover-spin').hide();
}



function cargar() {
    var pValor = "Jorge";  //$('#txt_nombre').val();    

    _type = "GET";
    _contentType = "application/json; charset=utf-8"; //"application/x-www-form-urlencoded; charset=UTF-8";//"application/json";json
    _url = 'Bag.aspx/WebM_Prueba'; //?pNom=' + pValor;

    var request = {
        type: _type,
        contentType: _contentType,
        url: _url,
        params: "{}",
        success: 'cargar_final',
        error: 'ServiceFailed',
        dataType: "json"
    };
    CallService(request);
}

//Funcion que dibuja el resultado de la busqueda
function cargar_final() {
    var _obj = g_XMLHttpRequest.resultado.d;
    var _msj = _obj.model;
    //var rows = "", _estado="";
    if (_obj.success == true) {

        //for (_i = 0; _i < _obj.length; _i++) {
        //    _estado = rTrim(_obj[_i].Estado);           

        //}
        //$("#tabla tbody").empty();
        //$('#tabla tbody').append(rows);
        alert(_obj.model);
    }
    else {
        alert(_msj);
    }
}

  //window.setTimeout(function () {
    //      $(".alert").fadeTo(1300, 0).slideUp(500); }, 1900);