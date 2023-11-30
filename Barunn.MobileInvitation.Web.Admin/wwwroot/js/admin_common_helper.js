
function TimeChk() {
    var html = [];
    var value = "";

    for (var i = 0; i < 24; i++) {
        if (i < 10) {
            value = "0" + i;
        }
        else {
            value = i;
        }
        html[i] = "<option value=" + value + ">" + value + "</option>"

        //if (vu != "" && vu == value) {
        //    html[i] = "<option value=" + value + " seleted>" + value + "</option>"
        //}
        //else html[i] = "<option value=" + value + ">" + value + "</option>"
     
    }
    return html;
}


var isEmpty = function (val) {
    if (val === "" || val === null || val === undefined
        || (val !== null && typeof val === "object" && !Object.keys(val).length)
    ) {
        return true
    } else {
        return false
    }

};

function displayValidationErrors(errors) {
    var $ul = $('div.validation-summary-valid.text-danger > ul');

    $ul.empty();
    $.each(errors, function (idx, errorMessage) {
        $ul.append('<li>' + errorMessage + '</li>');
    });
};


var newGuid = function () {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g,
        function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
};
function toStringByFormatting(source, delimiter = '-') {
    const year = source.getFullYear();
    const month = leftPad(source.getMonth() + 1);
    const day = leftPad(source.getDate());

    return [year, month, day].join(delimiter);
}

function leftPad(value) {
    if (value >= 10) {
        return '' + value;
    }
    return '0' + value;
}