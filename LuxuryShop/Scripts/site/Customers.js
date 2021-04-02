//Function to search the most visitors with 2 dates given:
function searchVisitors(pTopVisitors, pFrom, pTo) {
    

    _type = "POST";
    _contentType = "application/json; charset=utf-8"; 
    _url = 'Customers.aspx/WebM_VisitorsByDate';
    let obj = {};
    obj.pTopVisitors = $.trim(pTopVisitors);
    obj.pFecFrom = $.trim(pFrom);
    obj.pFecTo = $.trim(pTo);

    var request = {
        type: _type,
        contentType: _contentType,
        url: _url,
        params: JSON.stringify(obj),
        success: 'searchVisitors_final',
        error: 'ServiceFailed',
        dataType: "json"
    };
    CallService(request);
}

//Function with te result of the operation.
function searchVisitors_final() {
    let _obj = g_XMLHttpRequest.resultado.d;
    let _searchParams = new URLSearchParams(window.location.search)    
    let _visitors;
       
    if (_obj.success == true) {
        //alert(_obj.msg);
        $('#cover-spin').hide();
        $("#tblVisitors > tbody").html("");
        _visitors = $.parseJSON(_obj.model);
        $.each(_visitors, function (i, item) {
            
            
            $('#tblBodyVisitors').append('<tr><td scope="row" style="text-align: center;">' + (i+1) + '</td><td style="text-align: center;">' + item.name + '</td><td style="text-align: center;">' +item.lastName + '</td><td style="text-align: center;">' + item.email + '</td><td style="text-align: center;">' + item.address + '</td></tr>');
               
           
        });
        showMsg(_obj.success, _obj.msg);

    }
    else if ( _obj.msg.indexOf("1") >= 0) {       
       //alert(_obj.msg);
        $('#cover-spin').hide();
        showMsg(_obj.success, _obj.msg);
    }
    else {
        $('#cover-spin').hide();
        showMsg(_obj.success, _obj.msg);
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
   
    
}

//Function to validate the format of the date  ^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$
function isValidDate(dateString) {
    var regEx = /^\d{4}-\d{2}-\d{2}$/;
    if (!dateString.match(regEx)) return false;  // Invalid format
    var d = new Date(dateString);
    var dNum = d.getTime();
    if (!dNum && dNum !== 0) return false; // NaN value, Invalid date
    return d.toISOString().slice(0, 10) === dateString;
}
