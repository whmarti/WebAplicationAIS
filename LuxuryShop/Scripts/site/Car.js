let oper = "";
function updItem(pIdCarItem, pQty, pOper) {
    oper = pOper;
    _type = "POST";
    _contentType = "application/json; charset=utf-8";
    _url = 'Car.aspx/WebM_UpdCarItem';
    let obj = {};
    obj.pIdCarItem = $.trim(pIdCarItem);
    obj.pQty = $.trim(pQty);
    obj.pOper = pOper;

    var request = {
        type: _type,
        contentType: _contentType,
        url: _url,
        params: JSON.stringify(obj),
        success: 'updItem_final',
        error: 'ServiceFailed',
        dataType: "json"
    };
    CallService(request);
}

//Function with te result of the operation.
function updItem_final() {
    var _obj = g_XMLHttpRequest.resultado.d;
    let _searchParams = new URLSearchParams(window.location.search)

    if (_obj.success == true) {
        showMsg(_obj.success, _obj.msg);
        if(oper="DEL")
           setTimeout(function () { location.href = "Car.aspx"; }, 4000);
    }
    else if (_obj.msg.indexOf("1") >= 0) {
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


function buy(pAddress) {
    if (!window.confirm("Do you really want to make the purchase and receive it at this address:\n" + pAddress + "?")) {
        return;
    }
    $('#cover-spin').show(0);   
    $(".spinner").css("visibility", "visible");    

    _type = "POST";
    _contentType = "application/json; charset=utf-8";
    _url = 'Car.aspx/WebM_createOrder';
    let obj = {};
    obj.pIdUser = $('#idUser').val();

    var request = {
        type: _type,
        contentType: _contentType,
        url: _url,
        params: JSON.stringify(obj),
        success: 'buy_final',
        error: 'ServiceFailed',
        dataType: "json"
    };
    CallService(request);
}

//Function with te result of the operation.
function buy_final() {
    var _obj = g_XMLHttpRequest.resultado.d;
    let _searchParams = new URLSearchParams(window.location.search)

    if (_obj.success == true) {
        showMsg(_obj.success, _obj.msg);
        setTimeout(function () { location.href = "../Index.aspx"; }, 4000);
    }
    else if (_obj.msg.indexOf("-1") >= 0) {
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
        if ((_obj.msg.indexOf("Error") >= 0) && (!_obj.success)) {
            showMsg(_obj.success,"Operation failed, purchase not made. Try again.");
        }
    }
    //$(".spinner").css("visibility", "hidden");
    $('#cover-spin').hide();
}

function delItem(pIdCarItem, pQty) {
    _type = "POST";
    _contentType = "application/json; charset=utf-8";
    _url = 'Car.aspx/WebM_UpdCarItem';
    let obj = {};
    obj.pIdCarItem = $.trim(pIdCarItem);
    obj.pQty = $.trim(pQty);
    obj.pOper = "DEL";

    var request = {
        type: _type,
        contentType: _contentType,
        url: _url,
        params: JSON.stringify(obj),
        success: 'updItem_final',
        error: 'ServiceFailed',
        dataType: "json"
    };
    CallService(request);
}

//Function to remove result of the operation.
function delItem_final() {
    var _obj = g_XMLHttpRequest.resultado.d;
    let _searchParams = new URLSearchParams(window.location.search)

    if (_obj.success == true) {
        showMsg(_obj.success, _obj.msg);
        location.href = "car.aspx";
    }
    else if (_obj.msg.indexOf("1") >= 0) {
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

//Function for validate numeric values in the text field
function fn_validateNumeric(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }

}