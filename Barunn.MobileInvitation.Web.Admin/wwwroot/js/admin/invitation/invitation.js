var invitation_areas = [];
var area = {};
var objects = [];
var mappingfield = {};
var tb_area = [];

var keyStop = {
    8: ":not(input:text, textarea, input:file, input:password)", // stop backspace = back
    13: "input:text, input:password", // stop enter = submit 
    end: null
};

var item = function () {
    return {
        item_id: '',
        resource_id: '',
        pid: '',
        id: '',
        type: '',
        top: 0,
        left: 0,
        height: 0,
        width: 0,
        chracterset: '',
        fontsize: 0,
        fontcolor: '#000000',
        bgcolor: '',
        bold_yn: false,
        italic_yn: false,
        underline_yn: false,
        between_text: 0,
        between_line: 1.4,
        vertical_align: '',
        horizontal_align: '',
        zindex: 0,
        font: "'Noto Sans KR', sans-serif",
        bring_front: false,
        bring_forward: false,
        send_back: false,
        send_backward: false,
        resource_url: '',
        resource_absoluteurl: '',
        org_height: 0,
        org_width: 0
    };
}


var invitation = {
    idx: 0,
    id: null,
    width: 0,
    height: 0,
    x: 0,
    y: 0,

    addImage: function (e) {
        tempFileUpload('Image');
    },
    addImageEvent: function (e) {

        $('#' + invitation.id).resizable({
            handles: 'n, e, s, w, sw, nw, ne, se',
            containment: '#' + area.id,
            start: function (event, ui) {
                var handle = $(this).data('ui-resizable').axis;
                var maintain_aspect_ratio = (["n", "e", "s", "w"].indexOf(handle) == -1);
                $(this).resizable("option", "aspectRatio", maintain_aspect_ratio)
                    .data('ui-resizable')
                    ._aspectRatio = maintain_aspect_ratio;
            },
            resize: function (event, ui) {
                var hr = $(this).outerHeight();
                var wr = $(this).outerWidth();
                $(this).find(".img").css({ "width": wr, "height": hr });
                $(this).children(".ui-resizable-handle").removeClass('resizabled');
                invitation.pid = $("#" + invitation.id).parent('div').attr('id');
                invitation.showInfo($(this), 'img');
            },
            stop: function (event, ui) {
                $(this).children(".ui-resizable-handle").addClass('resizabled')
                $(this).find($('.img-info')).css('display', 'none');
                invitation.changeObject($(this), 'img');
            }
        });

        $('#' + invitation.id).draggable({
            containment: "#" + area.id,
            cursor: "move",
            snap: ".invitationarea",
            snapTolerance: 1,
            drag: function (event, ui) {
                $(this).children(".ui-resizable-handle").removeClass('resizabled');
                invitation.showInfo($(this), 'img');
                $(".guideline").show();
            },
            stop: function (event, ui) {
                $(this).children(".ui-resizable-handle").addClass('resizabled');
                $(this).find($('.img-info')).css('display', 'none');
                invitation.changeObject($(this), 'img');
                $(".guideline").hide();
            }
        });

        $('#' + invitation.id).mousedown(function (e) {

            $(".ui-resizable-handle").removeClass('resizabled');
            $(".resizable").removeClass("selected");
            $(this).addClass("selected");
            $(this).children(".ui-resizable-handle").addClass('resizabled');
            $(".ui-resizable-handle").hide();
            $(this).children(".ui-resizable-handle").show();

            area.id = $(this).parent('div').attr('id')
            invitation.id = $(this).attr('id');
            invitation.x = e.pageX - $(this).parent('div').offset().left;
            invitation.y = e.pageY - $(this).parent('div').offset().top;
            $(".matchinfo").removeClass("selected");
            invitation.imageMode();
            invitation.showInfo($(this), 'img');
            e.stopPropagation();

        });

        $('#' + invitation.id).mouseup(function (e) {
            $(this).find($('.img-info')).css('display', 'none');
        });

        $(".ui-resizable-handle").removeClass('ui-icon ui-icon-gripsmall-diagonal-se');

        $('#' + invitation.id).children(".ui-resizable-handle").addClass('resizabled');

    },
    setInvitationIdx: function () {
        if ($('.item').length > 0) {
            var tmp_idx = 1;
            $('.item').each(function (index, obj) {
                tmp_idx = parseInt($("#" + obj.id).attr('idx')) + 1;
                if (invitation.idx < tmp_idx) {
                    invitation.idx = tmp_idx;
                }
            });
        } else {
            invitation.idx = 1;
        }
    },
    changeText: function (target) {
        objects.forEach(function (elem) {
            if (elem.id == $(".item.selected").attr('id') && $(".matchinfo.selected").attr('idx') == $(target).attr('idx')) {
                elem.chracterset = $(target).val();
            }
        });
        var n = $(target).attr('idx');
        var text = invitation.matchText($(target).val());
        $('#item_' + n + ' .text').html(text);
    },
    changeTextCss: function (target) {

        objects.forEach(function (elem) {
            if (elem.id == $(".item.selected").attr('id')) {
                switch ($(target).attr('id')) {
                    case 'FontSize':
                        elem.fontsize = parseInt($("#FontSize").val());
                        $('.item.selected>.text').css('font-size', parseInt($("#FontSize").val()));
                        break;
                    case 'Bold':
                        elem.bold_yn = $("#Bold").hasClass("selected") ? true : false;
                        $('.item.selected>.text').css('font-weight', $("#Bold").hasClass("selected") ? "bold" : "");
                        break;
                    case 'Italic':
                        elem.italic_yn = $("#Italic").hasClass("selected") ? true : false;
                        $('.item.selected>.text').css('font-style', $("#Italic").hasClass("selected") ? "italic" : "");
                        break;
                    case 'Underline':
                        elem.underline_yn = $("#Underline").hasClass("selected") ? true : false;
                        $('.item.selected>.text').css('text-decoration', $("#Underline").hasClass("selected") ? "underline" : "");
                        break;
                    case 'Left':
                        elem.horizontal_align = $("#Left").hasClass("selected") ? "L" : elem.horizontal_align;
                        $('.item.selected>.text').css('text-align', $("#Left").hasClass("selected") ? "left" : "");
                        break;
                    case 'Center':
                        elem.horizontal_align = $("#Center").hasClass("selected") ? "C" : elem.horizontal_align;
                        $('.item.selected>.text').css('text-align', $("#Center").hasClass("selected") ? "center" : "");
                        break;
                    case 'Right':
                        elem.horizontal_align = $("#Right").hasClass("selected") ? "R" : elem.horizontal_align;
                        $('.item.selected>.text').css('text-align', $("#Right").hasClass("selected") ? "right" : "");
                        break;
                    case 'Top':
                        elem.vertical_align = $("#Top").hasClass("selected") ? "T" : elem.vertical_align;
                        $('.item.selected').css('align-items', $("#Top").hasClass("selected") ? "flex-start" : "");
                        break;
                    case 'Middle':
                        elem.vertical_align = $("#Middle").hasClass("selected") ? "M" : elem.vertical_align;
                        $('.item.selected').css('align-items', $("#Middle").hasClass("selected") ? "center" : "");
                        break;
                    case 'Bottom':
                        elem.vertical_align = $("#Bottom").hasClass("selected") ? "B" : elem.vertical_align;
                        $('.item.selected').css('align-items', $("#Bottom").hasClass("selected") ? "flex-end" : "");
                        break;
                    case 'Between_Text_Calc':
                        elem.between_text = parseFloat($("#Between_Text").val());
                        $('.item.selected>.text').css('letter-spacing', parseFloat($("#Between_Text").val()) / 100 + "em");
                        break;
                    case 'Between_Line_Calc':
                        elem.between_line = parseFloat($("#Between_Line").val());
                        $('.item.selected>.text').css('line-height', parseFloat($("#Between_Line").val()) + "em");
                        break;
                    case 'Between_Text':
                        elem.between_text = parseFloat($("#Between_Text_Calc").val());
                        $('.item.selected>.text').css('letter-spacing', parseFloat($("#Between_Text").val()) / 100 + "em");
                        break;
                    case 'selFont':
                        elem.font = $("#selFont").val();
                        $('.item.selected>.text').css('font-family', $("#selFont").val());
                        break;
                }

                elem.width = $(".item.selected>.text").width();
                elem.height = $(".item.selected>.text").height();

                invitation.pid = $(".item.selected").parent('div').attr('id');

            }
        });

        $('.item').each(function () {
            $(this).data("height", $(this).outerHeight());
            $(this).data("width", $(this).outerWidth());
        });
        $('.text', '.item').each(function () {
            $(this).data("height", $(this).outerHeight());
            $(this).data("width", $(this).outerWidth());
            $(this).data("fontSize", parseInt($(this).css("font-size")));
        });

    },
    matchText: function (text) {

        if (text != null) {

            var _matches = text.match(/#[^#]+#/g);


            if (_matches != null) {
                for (var i = 0; i < _matches.length; i++) {
                    var target = _matches[i].replace(/#/g, '');
                    var split = target.split(/\|/);
                    var _append = $('[match="' + split[0] + '"]').val();

                    if (split.length > 1) {
                        _append = '<span style="' + split[1] + '">' + _append + '</span>'
                    }
                    text = text.replace(_matches[i], _append);
                }
            }
            text = text.replace(/\r|\n|\r\n/g, "<br>");

        }
        return text;
    },
    addText: function (key, e) {

        invitation.setInvitationIdx();

        var text = invitation.matchText(key);

        invitation.id = 'item_' + invitation.idx;
        invitation.txtid = 'addtext_' + invitation.idx;
        var div = "<div id='" + invitation.id + "' idx='" + invitation.idx + "' class='item ui-widget-content selected resizable' style='top: " + invitation.y + "px; left: " + invitation.x + "px; position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><div class='text'  >" + text + "</div><span class='txt-info'></span></div>";
        var matchinfo = "<input type='text' id='" + invitation.txtid + "' idx='" + invitation.idx + "' class='form-control form-control-sm matchinfo selected' value='" + key + "'>"


        $('#' + area.id).append(div);
        $('#divMatch').append(matchinfo);

        $("#" + invitation.txtid).on("focus", function (e) {
            $(document).unbind('keydown');
        });
        $("#" + invitation.txtid).on("blur", function (e) {
            $(document).on("keydown", function (event) {
                if (event.keyCode == 46) {

                    if ($(".item.selected").hasClass('photo-image')) {
                        $('#btnphotoupload').addClass('btn-primary');
                        $('#btnphotoupload').removeClass('btn-secondary');
                        $('#btnphotoupload').addClass('empty');
                        $('#btnphotoupload').text('추가');
                    }

                    invitation.pid = $(".item.selected").parent('div').attr('id');
                    $(".item.selected").remove();
                    $(".matchinfo.selected").remove();
                    invitation.delObject();
                    invitation.initMode();
                    if ($("#" + invitation.pid + " .item").length < 1) {
                        invitation.resizeArea('delete', invitation.pid);
                    }
                    invitation.showPlaceholder($('#' + invitation.pid));
                }
            });
        });

        $('.item.selected>.text').css('font-family', "'Noto Sans KR', sans-serif");

        invitation.org_height = parseFloat($('#' + invitation.id).outerHeight());
        invitation.org_width = parseFloat($('#' + invitation.id).outerWidth());
        invitation.height = parseFloat($('#' + invitation.id).outerHeight());
        invitation.width = parseFloat($('#' + invitation.id).outerWidth());
        invitation.characterset = key;

        invitation.addObject($(this), 'txt');

        $('.item').each(function () {
            $(this).data("height", $(this).outerHeight());
            $(this).data("width", $(this).outerWidth());
        });
        $('.text', '.item').each(function () {
            $(this).data("height", $(this).outerHeight());
            $(this).data("width", $(this).outerWidth());
            $(this).data("fontSize", parseInt($(this).css("font-size")));
        });

        if (key == "#인사말#") {
            $("#Center").trigger('click');
            $("#FontSize").val(34);
            $('#FontSize').trigger('change');
            $("#Between_Line_Calc").val(2.3);
            $("#Between_Line_Calc").trigger('change');
        } else {
            $("#Left").trigger('click');
        }
        $("#Middle").trigger('click');

        invitation.textMode();
        invitation.addTextEvent();

        invitation.hidePlaceholder($('#' + area.id));
    },
    addTextEvent: function (e) {
        $("#" + invitation.id).resizable({
            handles: 'n, e, s, w, sw, nw, ne, se',
            containment: '#' + area.id,
            start: function (event, ui) {
                var handle = $(this).data('ui-resizable').axis;
                var maintain_aspect_ratio = (["n", "e", "s", "w"].indexOf(handle) == -1);
                $(this).resizable("option", "aspectRatio", maintain_aspect_ratio)
                    .data('ui-resizable')
                    ._aspectRatio = maintain_aspect_ratio;
            },
            resize: function (event, ui) {
                invitation.pid = $("#" + invitation.id).parent('div').attr('id');
                $("#FontSize").val(parseInt($(this).children('.text').css("font-size")));
                invitation.showInfo($(this), 'txt');
            },
            stop: function (event, ui) {
                $(this).find($('.txt-info')).css('display', 'none');
                invitation.changeObject($(this), 'txt');
            }

        });

        $('#' + invitation.id).draggable({
            containment: "#" + area.id,
            cursor: "move",
            snap: ".invitationarea",
            snapTolerance: 1,
            start: function (event, ui) {

                $(".ui-resizable-handle").removeClass('resizabled');
            },
            drag: function (event, ui) {
                invitation.showInfo($(this), 'txt');

                $(".guideline").show();
            },
            stop: function (event, ui) {
                $(this).children(".ui-resizable-handle").addClass('resizabled');
                $(this).find($('.txt-info')).css('display', 'none');
                invitation.changeObject($(this), 'txt');
                $(".guideline").hide();
            }
        });

        $('#' + invitation.id).mousedown(function (e) {
            $(".ui-resizable-handle").removeClass('resizabled');
            $(".resizable").removeClass("selected");
            $(this).addClass("selected");
            $(this).children(".ui-resizable-handle").addClass('resizabled');
            $(".ui-resizable-handle").hide();
            $(this).children(".ui-resizable-handle").show();

            area.id = $(this).parent('div').attr('id')
            invitation.id = $(this).attr('id');
            invitation.x = e.pageX - $(this).parent('div').offset().left;
            invitation.y = e.pageY - $(this).parent('div').offset().top;

            $(".matchinfo").removeClass("selected");
            $(".matchinfo[idx=" + $(this).attr('idx') + "]").addClass("selected");

            invitation.textMode();

            invitation.showInfo($(this), 'txt')

            e.stopPropagation();
        });

        $('#' + invitation.id).mouseup(function (e) {
            $(this).find($('.txt-info')).css('display', 'none');
        });

        $(".ui-resizable-handle").removeClass('ui-icon ui-icon-gripsmall-diagonal-se');

        $('#' + invitation.id).children(".ui-resizable-handle").addClass('resizabled');
    },
    bring_front: function (e) {
      
        var pid = $(".item.selected").parent('div').attr('id');
        $("#" + pid + " > .item").last().after($(".item.selected"));
        invitation.initObject();
    },
    bring_forward: function (e) {
        var pid = $(".item.selected").parent('div').attr('id');
        $("#" + pid + " > .item.selected").next().after($(".item.selected"));
        invitation.initObject();
    },
    send_back: function (e) {
        var pid = $(".item.selected").parent('div').attr('id');
        $("#" + pid + " > .item").first().before($(".item.selected"));
        invitation.initObject();
    },
    send_backward: function (e) {
        var pid = $(".item.selected").parent('div').attr('id');
        $("#" + pid + " > .item.selected").prev().before($(".item.selected"));
        invitation.initObject();
    },
    changeObject: function (ui, type) {
        invitation.x = parseFloat(ui.position().left);
        invitation.y = parseFloat(ui.position().top);
        invitation.width = parseFloat(ui.outerWidth());
        invitation.height = parseFloat(ui.outerHeight());

        if (type == 'txt') {
            invitation.fontsize = parseInt(ui.children('.text').css("font-size"));
        } else {
            invitation.fontsize = 0;
        }

        objects.forEach(function (elem) {
            if (elem.id == invitation.id) {
                elem.left = invitation.x
                elem.top = invitation.y
                elem.width = invitation.width
                elem.height = invitation.height
                elem.fontsize = invitation.fontsize
            }
        });

    },
    addObject: function (ui, type) {

        var obj = new item();

        obj.id = $(this).attr('id');

        var idx = 0;
        objects.forEach(function (elem) {
            if (elem.pid == $("#" + obj.id).parent('div').attr('id')) {
                idx++;
            }
        });

        obj.type = type
        obj.zindex = idx + 1;
        obj.pid = $("#" + obj.id).parent('div').attr('id');
        obj.top = invitation.y;
        obj.left = invitation.x;
        obj.org_width = invitation.org_width;
        obj.org_height = invitation.org_height;
        obj.width = invitation.width;
        obj.height = invitation.height;


        if (type == 'img' || type == 'photo' || type == 'profile') {

            obj.resource_url = invitation.resource_url;
            obj.resource_absoluteurl = invitation.resource_absoluteurl;
            obj.fontsize = 0;
        }

        if (type == 'txt') {
            obj.fontsize = parseInt($('html').css("font-size"));
            obj.chracterset = invitation.characterset;
        }

        objects.push(obj);
        invitation.initObject();
    },
    initObject: function (e) {

        var idx = 1;
        $('.item').each(function (e) {
            var obj = new item();
            obj.id = $(this).attr('id');
            var objidx = objects.findIndex(v => v.id == obj.id && v.pid == $("#" + obj.id).parent('div').attr('id'));
            if (objidx > -1) {
                objects[objidx].zindex = idx;
                invitation.resetLayerButton(objects[objidx]);
                invitation.setLayerStatus();
                idx++;
            }
        });
    },
    delObject: function (e) {

        var tmp_objects = [];

        var idx = 1;

        $('.item').each(function (e) {
            var obj = new item();
            obj.id = $(this).attr('id');
            objects.forEach(function (elem) {
                if (elem.pid == $("#" + obj.id).parent('div').attr('id') && elem.id == obj.id) {
                    elem.zindex = idx;
                    invitation.resetLayerButton(elem);
                    tmp_objects.push(elem);
                }
            });
            idx++;

        });
        objects = tmp_objects;
    },
    resetLayerButton: function (elem) {
        elem.bring_front = $("#" + elem.id).attr('id') == $("#" + elem.pid + " > .item").last().attr('id') ? false : true;
        elem.bring_forward = $("#" + elem.id).attr('id') == $("#" + elem.pid + " > .item").last().attr('id') ? false : true;
        elem.send_back = $("#" + elem.id).attr('id') == $("#" + elem.pid + " > .item").first().attr('id') ? false : true;
        elem.send_backward = $("#" + elem.id).attr('id') == $("#" + elem.pid + " > .item").first().attr('id') ? false : true;
    },
    showInfo: function (ui, type) {

        if (parseInt(ui.position().left) + parseInt(ui.outerWidth()) > 665) {
            $('.img-info').css({ "right": 0 });
            $('.txt-info').css({ "right": 0 });
        } else {
            $('.img-info').css({ "right": -210 });
            $('.txt-info').css({ "right": -210 });
        }

        var z_index = 0;
        for (var a = 0; a < objects.length; a++) {
            if ((objects[a].id) == ui.attr('id')) {
                z_index = objects[a].zindex;
                break;
            }
        }
        if (type == 'txt') {
            ui.find($('.txt-info')).html('ㅤLEFT : ' + parseInt(ui.position().left) + 'ㅤ/ㅤTOP : ' + parseInt(ui.position().top) + '<br/>ㅤWIDTH : ' + parseInt(ui.outerWidth()) + 'ㅤ/ㅤHIEGHT : ' + parseInt(ui.outerHeight()) + '<br/>ㅤFONT-SIZE : ' + parseInt(ui.children('.text').css("font-size")) + '<br/>ㅤZ-INDEX : ' + z_index);
            ui.find($('.txt-info')).css('display', 'block');
        }
        else {
            ui.find($('.img-info')).html('ㅤLEFT : ' + parseInt(ui.position().left) + 'ㅤ/ㅤTOP : ' + parseInt(ui.position().top) + '<br/>ㅤWIDTH : ' + parseInt(ui.outerWidth()) + 'ㅤ/ㅤHIEGHT : ' + parseInt(ui.outerHeight()) + '<br/>ㅤZ-INDEX : ' + z_index);
            ui.find($('.img-info')).css('display', 'block');
        }
    },
    initClass: function () {
        $(".ui-resizable-handle").hide();
        $(".invitationarea").removeClass("selected");
        $(".ui-resizable-handle").removeClass('resizabled');
        $(".resizable").removeClass("selected");
        $(".resizable").removeClass("ui-widget-content");
    },
    textMode: function () {
        $("#divDisabled1").css("display", "none");
        $("#divDisabled2").css("display", "none");
        $("#divDisabled3").css("display", "none");
        $("#divDisabled4").css("display", "none");
        $("#divDisabled5").css("display", "none");

        $('.dvTool a').removeClass('disabled');
        $('.dvTool select').removeAttr("disabled");
        $('.dvTool input ').removeAttr("disabled");
        invitation.setTextStatus();
    },
    imageMode: function () {
        invitation.initMode();

        $("#divDisabled4").css("display", "none");

        invitation.setImageStatus();
    },
    initMode: function () {
        $("#divDisabled1").css("display", "block");
        $("#divDisabled2").css("display", "block");
        $("#divDisabled3").css("display", "block");
        $("#divDisabled4").css("display", "block");
        $("#divDisabled5").css("display", "block");

        $("#FontSize").val(16);
        $("#Bold").removeClass("selected");
        $("#Italic").removeClass("selected");
        $("#Underline").removeClass("selected");
        $("#Left").removeClass("selected");
        $("#Center").removeClass("selected");
        $("#Right").removeClass("selected");
        $("#Top").removeClass("selected");
        $("#Middle").removeClass("selected");
        $("#Bottom").removeClass("selected");
        $("#object_txtcolor").val("#000000");
        $("#Between_Text_Calc").val(0);
        $("#Between_Text").val(0);
        $("#Between_Line_Calc").val(1.4);
        $("#Between_Line").val(1.4);
    },
    setTextStatus: function () {

        var id = $('.item.selected').attr('id');

        objects.forEach(function (elem) {


            if (elem.id == id) {

                $("#selFont").val(elem.font);

                $("#FontSize").val(elem.fontsize);
                $("#Between_Text_Calc").val(elem.between_text);
                $("#Between_Text").val(elem.between_text);
                $("#Between_Line_Calc").val(elem.between_line);
                $("#Between_Line").val(elem.between_line);

                elem.bold_yn ? $("#Bold").addClass("selected") : $("#Bold").removeClass("selected");
                elem.italic_yn ? $("#Italic").addClass("selected") : $("#Italic").removeClass("selected");
                elem.underline_yn ? $("#Underline").addClass("selected") : $("#Underline").removeClass("selected");

                $("#Left").removeClass("selected");
                $("#Center").removeClass("selected");
                $("#Right").removeClass("selected");
                switch (elem.horizontal_align) {
                    case "L":
                        $("#Left").addClass("selected");
                        break;
                    case "C":
                        $("#Center").addClass("selected");
                        break;
                    case "R":
                        $("#Right").addClass("selected");
                        break;
                }
                switch (elem.vertical_align) {
                    case "T":
                        $("#Top").addClass("selected");
                        break;
                    case "M":
                        $("#Middle").addClass("selected");
                        break;
                    case "B":
                        $("#Bottom").addClass("selected");
                        break;
                }

                $("#object_txtcolor").val(elem.fontcolor);
                $("#object_bgcolor").val(elem.bgcolor);


                invitation.resetLayerButton(elem);

                elem.bring_front ? $("#Bring_Front").removeClass('disabled') : $("#Bring_Front").addClass('disabled');
                elem.bring_forward ? $("#Bring_Forward").removeClass('disabled') : $("#Bring_Forward").addClass('disabled');
                elem.send_back ? $("#Send_Back").removeClass('disabled') : $("#Send_Back").addClass('disabled');
                elem.send_backward ? $("#Send_Backward").removeClass('disabled') : $("#Send_Backward").addClass('disabled');
            }
        });
    },
    copyTitle: function (remittitleImage) {
        var area12hasobjects = objects.findIndex(v => v.pid === "area12");
        if (area12hasobjects == -1) {
            var remittitleImage = "/img/title/tit_remittance.png";
            var prodCateCode = $("#Product_Category_Code").val();
            if (prodCateCode == "PCC03") {
                remittitleImage = "/img/title/tit_remittance_PCC03.png";
            }

            area.id = "area12";
            invitation.resource_url = $("#MoneyGift_Remit_Title_URL").val();
            invitation.width = 800;
            invitation.height = 223;
            invitation.y = 0;
            invitation.x = 0;
            invitation.idx = 1;

            $('#' + area.id).css('height', invitation.height + "px");

            invitation.id = 'item_' + invitation.idx;
            var div = "<div id='" + invitation.id + "' idx='" + invitation.idx + "' class='item ui-widget-content selected resizable' style='top: " + invitation.y + "px; left: " + invitation.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + invitation.resource_url + "' width='" + invitation.width + "px' height='" + invitation.height + "px'  /><span class='img-info'></span></div>";

            $('#' + area.id).append(div);
            invitation.addObject($(this), 'img');
            //invitation.imageMode();
            invitation.addImageEvent();
            invitation.hidePlaceholder($('#' + area.id));

            $(".ui-resizable-handle").removeClass('resizabled');
            $(".resizable").removeClass("selected");
            $(".ui-resizable-handle").hide();

        }
        
    },
    setImageStatus: function () {
        var id = $('.item.selected').attr('id');

        objects.forEach(function (elem) {
            if (elem.id == id) {

                invitation.resetLayerButton(elem);

                elem.bring_front ? $("#Bring_Front").removeClass('disabled') : $("#Bring_Front").addClass('disabled');
                elem.bring_forward ? $("#Bring_Forward").removeClass('disabled') : $("#Bring_Forward").addClass('disabled');
                elem.send_back ? $("#Send_Back").removeClass('disabled') : $("#Send_Back").addClass('disabled');
                elem.send_backward ? $("#Send_Backward").removeClass('disabled') : $("#Send_Backward").addClass('disabled');
            }
        });
    },
    setLayerStatus: function () {

        var id = $('.item.selected').attr('id');

        objects.forEach(function (elem) {

            if (elem.id == id) {
                elem.bring_front ? $("#Bring_Front").removeClass('disabled') : $("#Bring_Front").addClass('disabled');
                elem.bring_forward ? $("#Bring_Forward").removeClass('disabled') : $("#Bring_Forward").addClass('disabled');
                elem.send_back ? $("#Send_Back").removeClass('disabled') : $("#Send_Back").addClass('disabled');
                elem.send_backward ? $("#Send_Backward").removeClass('disabled') : $("#Send_Backward").addClass('disabled');
            }
        });
    },
    resizeArea: function (type, area_id) {

        var maxObjectSize = 100;

        $("#" + area_id).children('.item').each(function () {
            var a = parseInt($(this).position().top) + parseInt($(this).outerHeight());

            if (maxObjectSize < a) {
                maxObjectSize = a;
            }
        });

        if (type == 'resize') {
            if (parseFloat($('.item.selected').position().top) + parseFloat($('.item.selected').outerHeight()) > maxObjectSize) {
                $('.item.selected').parent('div').css('height', parseFloat($('.item.selected').position().top) + parseFloat($('.item.selected').outerHeight()))
            } else {
                $('.item.selected').parent('div').css('height', maxObjectSize)
            }
        } else {
            $("#" + area_id).css('height', maxObjectSize)
        }
    },
    setItem: function () {
        objects.forEach(function (elem) {
            invitation.idx++;
            invitation.id = elem.id;
            invitation.pid = elem.pid;
            invitation.x = elem.left;
            invitation.y = elem.top;
            invitation.resource_url = elem.resource_url;
            invitation.resource_absoluteurl = elem.resource_absoluteurl;
            invitation.width = elem.width;
            invitation.height = elem.height;
            if (elem.type == 'img') {
                var div = "<div id='" + invitation.id + "' idx='" + invitation.idx + "' class='item ui-widget-content resizable' style='top: " + invitation.y + "px; left: " + invitation.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + invitation.resource_absoluteurl + "' width='" + invitation.width + "px' height='" + invitation.height + "px'  /><span class='img-info'></span></div>";
                $('#' + invitation.pid).append(div);
                area.id = invitation.pid;
                invitation.addImageEvent();
                $('#' + invitation.id).children(".ui-resizable-handle").removeClass('resizabled');
                $('#' + invitation.id).children(".ui-resizable-handle").css('display', 'none');
                invitation.hidePlaceholder($('#' + area.id));
            }
            else if (elem.type == 'photo') {
                var div = "<div id='" + invitation.id + "' idx='" + invitation.idx + "' class='item ui-widget-content resizable photo-image ' style='top: " + invitation.y + "px; left: " + invitation.x + "px; width:" + invitation.width + "px; height:" + invitation.height + "px; position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' style='max-width:" + invitation.width + "px;max-height:" + invitation.height + "px;'><span class='img-info'></span></div>";
                $('#' + invitation.pid).append(div);
                invitation.addImageEvent();
                $('#' + invitation.id).children(".ui-resizable-handle").removeClass('resizabled');
                $('#' + invitation.id).children(".ui-resizable-handle").css('display', 'none');

                $('#btnphotoupload').removeClass('btn-primary');
                $('#btnphotoupload').addClass('btn-secondary');
                $('#btnphotoupload').removeClass('empty');
                $('#btnphotoupload').text('삭제');
                
                if ($("#Delegate_Image_AbsoluteUri").val() != "") {
                    $(".item.photo-image img").attr("src", $("#Delegate_Image_AbsoluteUri").val() + "?" + uuidv4()).css("width", "100%");
                }
                invitation.hidePlaceholder($('#' + area.id));

            } else if (elem.type == 'profile') {
                var div = "<div id='" + invitation.id + "' idx='" + invitation.idx + "' class='item ui-widget-content resizable profile-image ' style='top: " + invitation.y + "px; left: " + invitation.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + invitation.resource_absoluteurl + "' width='" + invitation.width + "px' height='" + invitation.height + "px'  /><span class='img-info'></span></div>";
                $('#' + invitation.pid).append(div);
                invitation.addImageEvent();
                $('#' + invitation.id).children(".ui-resizable-handle").removeClass('resizabled');
                $('#' + invitation.id).children(".ui-resizable-handle").css('display', 'none');

                $('#btnprofileupload').removeClass('btn-primary');
                $('#btnprofileupload').addClass('btn-secondary');
                $('#btnprofileupload').removeClass('empty');
                $('#btnprofileupload').text('삭제');
                invitation.hidePlaceholder($('#' + area.id));
            } else {
                var text = invitation.matchText(elem.chracterset);


                invitation.id = 'item_' + invitation.idx;
                invitation.txtid = 'addtext_' + invitation.idx;
                var div = "<div id='" + invitation.id + "' idx='" + invitation.idx + "' class='item ui-widget-content resizable' style='top: " + invitation.y + "px; left: " + invitation.x + "px;    position:absolute; width:" + invitation.width + "px;  height:" + invitation.height + "px;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><div class='text'>" + text + "</div><span class='txt-info'></span></div>";
                var matchinfo = "<input type='text' id='" + invitation.txtid + "' idx='" + invitation.idx + "' class='form-control form-control-sm matchinfo ' value='" + elem.chracterset + "'>"
                $('#' + invitation.pid).append(div);
                $('#divMatch').append(matchinfo);
                area.id = invitation.pid;
                invitation.addTextEvent();
                $('#' + invitation.id).css('background-color', elem.bgcolor);
                $('#' + invitation.id).children(".ui-resizable-handle").removeClass('resizabled');
                $('#' + invitation.id).children(".ui-resizable-handle").css('display', 'none');
                $('#' + invitation.id + ">.text").css('font-family', elem.font);
                $('#' + invitation.id + ">.text").css('font-size', elem.fontsize);
                $('#' + invitation.id + ">.text").css('color', elem.fontcolor);
                $('#' + invitation.id + ">.text").css('font-weight', elem.bold_yn ? "bold" : "");
                $('#' + invitation.id + ">.text").css('font-style', elem.italic_yn ? "italic" : "");
                $('#' + invitation.id + ">.text").css('text-decoration', elem.underline_yn ? "underline" : "");
                if (elem.horizontal_align == "C") {
                    $('#' + invitation.id + ">.text").css('text-align', "center")
                } else if (elem.horizontal_align == "R") {
                    $('#' + invitation.id + ">.text").css('text-align', "right")
                } else if (elem.horizontal_align == "L") {
                    $('#' + invitation.id + ">.text").css('text-align', "left");
                } else {
                    $('#' + invitation.id + ">.text").css('text-align', "");
                }
                if (elem.vertical_align == "T") {
                    $('#' + invitation.id).css('align-items', "flex-start")
                } else if (elem.vertical_align == "M") {
                    $('#' + invitation.id).css('align-items', "center")
                } else if (elem.vertical_align == "B") {
                    $('#' + invitation.id).css('align-items', "flex-end");
                } else {
                    $('#' + invitation.id).css('align-items', "");
                }
                $('#' + invitation.id + ">.text").css('letter-spacing', elem.between_text / 100 + "em");
                $('#' + invitation.id + ">.text").css('line-height', elem.between_line + "em");
                $('.item').each(function () {
                    $(this).data("height", $(this).outerHeight());
                    $(this).data("width", $(this).outerWidth());
                });
                $('.text', '.item').each(function () {
                    $(this).data("height", $(this).outerHeight());
                    $(this).data("width", $(this).outerWidth());
                    $(this).data("fontSize", parseInt($(this).css("font-size")));
                });
            }
        });
    },
    initItem: function () {
        $("#wrap").css('background-color', '');
        $(".invitationarea").css('height', '100px');
        $(".invitationarea").css('background-color', '');
        $(".invitationarea").removeAttr("color_val");
        $(".invitationarea").removeClass("selected");
        $(".invitationarea div").remove();
        $(".invitationarea").find('.placeholder').show();

        $("#Invitation_URL").val("");
        $("#Invitation_Title").val("");

        $("#divMatch input").remove();
        $(".w25 li p").removeClass("selected");
        $(".w25 li img").removeClass("selected");
        objects = [];


        $('.invitationarea').mousedown(function (e) {
            $(".ui-resizable-handle").removeClass('resizabled');
            $(".resizable").removeClass("selected");
            $(this).addClass("selected");
            $(this).children(".ui-resizable-handle").addClass('resizabled');
            $(".ui-resizable-handle").hide();
            $(this).children(".ui-resizable-handle").show();
            $('.ui-resizable-handle.ui-resizable-s.resizabled').css("display", "block");
            $('.ui-resizable-handle.ui-resizable-s.resizabled').css('height', 0);
            $('.ui-resizable-handle.ui-resizable-s.resizabled').css('bottom', -2);

            e.stopPropagation();
        });

        $('.invitationarea').resizable({
            handles: 's'
        });

        $('.ui-resizable-handle.ui-resizable-s').css("display", "none");


    },
    setArea: function (obj) {

        $("#wrap").find(">div").each(function () {
            $(this).css("display", "none");
        });

        var prodCateCode = $("#Product_Category_Code").val();
        tb_area = $.grep(obj, function (item, index) { return (item.Product_Category_Codes.includes(prodCateCode)) });
        if (prodCateCode == "PCC01") { //청첩
            $("#area4 .list_wrap").show();
        } else if (prodCateCode == "PCC02") { //감사
            $("#area4 .list_wrap").hide();
        }

        tb_area.forEach(function (elem) {
            $("#wrap").find(">div").each(function () {
                if ($(this).attr('idx') == elem.Area_ID) {
                    $("#wrap>div[idx='" + elem.Area_ID + "']").css("display", "block");

                    if ($(this).hasClass('invitationarea')) {

                        if (elem.Area_ID != 2) {
                            $('#area' + elem.Area_ID).css('height', elem.Size_Height + "px");
                        } else {

                            var space = 0
                            if (typeof ($('#area' + elem.Area_ID).closest('div').find('img')[0]) != "undefined") {
                                space = parseInt($('#area' + elem.Area_ID + ' .text').parent('div').css('top')) - $('#area' + elem.Area_ID).closest('div').find('img')[0].height + 30;
                            } else {
                                space = parseInt($('#area' + elem.Area_ID + ' .text').parent('div').css('top')) + 30;
                            }

                            var bottom = $('#area' + elem.Area_ID + ' img').eq(1).height() == null ? 0 : $('#area' + elem.Area_ID + ' img').eq(1).height();

                            var height = $('#area' + elem.Area_ID + ' img').height() + $('#area' + elem.Area_ID + ' .text').height() + bottom + space

                            $('#area' + elem.Area_ID).css('height', height);

                            if (typeof ($('#area' + elem.Area_ID).closest('div').find('img')[1]) != "undefined") {
                                var id = $('#area' + elem.Area_ID).closest('div').find('img').parent('div')[1].id;

                                var top = parseInt($('#area' + elem.Area_ID + ' img').height() + $('#area' + elem.Area_ID + ' .text').height() + space);

                                $("#" + id).css('top', top);
                            }
                        }

                        $('#area' + elem.Area_ID).css('background-color', elem.Color);
                        $('#area' + elem.Area_ID).attr('color_val', elem.Color);

                        invitation.hidePlaceholder($('#area' + elem.Area_ID));
                    }
                }
            });
        });

    },
    showPlaceholder: function (area) {
        if (area.find('.item').length == 0) {
            area.find('.placeholder').show();
        }
    },
    hidePlaceholder: function (area) {
        if (area.find('.item').length > 0) {
            area.find('.placeholder').hide();
        }
    }
}

var fonts = [
    { name: 'Noto Sans', key: "'Noto Sans KR', sans-serif" },
    { name: '나눔 고딕', key: "'Nanum Gothic', sans-serif" },
    { name: '나눔 고딕 코딩', key: "'Nanum Gothic Coding', monospace" },
    { name: '나눔 명조', key: "'Nanum Myeongjo', serif", },
    { name: '나눔 펜 스크립트', key: "'Nanum Pen Script', cursive" },
    { name: '나눔 브러쉬 스크립트', key: "'Nanum Brush Script', cursive" },
    { name: '나눔 바른펜', key: "'NanumBarunpen'" },
    { name: '나눔 바른 고딕', key: "'NanumBarunGothic'" },
    { name: '나눔 스퀘어 라운드', key: "'NanumSquareRound'" },
    { name: '나눔 스퀘어 라이트', key: "'NanumSquare'" },
    { name: '제주 고딕', key: "'Jeju Gothic', sans-serif" },
    { name: '제주 명조', key: "'Jeju Myeongjo', serif" },
    { name: '제주 한라산', key: "'Jeju Hallasan', cursive" },
    { name: '코펍 바탕', key: "'KoPub Batang', serif" },
    { name: '한나', key: "'Hanna', sans-serif" },
    { name: '아리따부리', key: "'Arita-buri-SemiBold'" },
    { name: '앵무부리', key: "'116angmuburi'" },
    { name: '에스코어드림Lt', key: "'S-CoreDream-3Light'" },
    { name: '에스코어드림Reg', key: "'S-CoreDream-4Regular'" },
    { name: 'Cinzel', key: "'Cinzel', serif" },
    { name: 'Cafe24 Surround Air', key: "'Cafe24SsurroundAir'" },
    { name: '평창평화체700', key: "'PyeongChangPeace-Bold'" },
    { name: '평창평화체300', key: "'PyeongChangPeace'" },
    { name: 'Eczar', key: "'Eczar'" },
    { name: 'Libre Baskerville', key: "'Libre Baskerville'" }
]
var font_cnt = 0;

$(document).ready(function () {
    
    //Enter로 Submit되지 않게.
    $("#frmSave").keydown(function (event) {
        var selector = keyStop[event.which];
        if (selector !== undefined && $(event.target).is(selector)) {
            event.preventDefault(); //stop event
        }
    });

    //Form submit
    $("#frmSave").submit(function (e) {
        var idx = 0;
        //Add Area data: InvitationAreas.~
        tb_area.forEach(function (elem) {
            $("div[id^=area]").each(function () {
                if ($(this).attr('idx') == elem.Area_ID) {
                    if ($(this).hasClass('invitationarea')) {
                        $("<input />").attr("type", "hidden").attr("name", "InvitationAreas[" + idx + "].Invitation_ID").attr("value", elem.Invitation_ID).appendTo("#frmSave");
                        $("<input />").attr("type", "hidden").attr("name", "InvitationAreas[" + idx + "].Area_ID").attr("value", elem.Area_ID).appendTo("#frmSave");
                        $("<input />").attr("type", "hidden").attr("name", "InvitationAreas[" + idx + "].Size_Height").attr("value", $(this).height()).appendTo("#frmSave");
                        $("<input />").attr("type", "hidden").attr("name", "InvitationAreas[" + idx + "].Size_Width").attr("value", $(this).width()).appendTo("#frmSave");
                        $("<input />").attr("type", "hidden").attr("name", "InvitationAreas[" + idx + "].Color").attr("value", $(this).attr("color_val")).appendTo("#frmSave");
                        $("<input />").attr("type", "hidden").attr("name", "InvitationAreas[" + idx + "].Sort").attr("value", elem.Area_ID).appendTo("#frmSave");
                    }
                    idx++;
                }
            });
        });
        //Add Item Resources TemplateItemResources.~
        idx = 0;
        objects.forEach(function (elem) {
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].item_id").attr("value", elem.item_id).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].resource_id").attr("value", elem.resource_id).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].pid").attr("value", elem.pid).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].id").attr("value", elem.id).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].type").attr("value", elem.type).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].top").attr("value", elem.top).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].left").attr("value", elem.left).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].height").attr("value", elem.height).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].width").attr("value", elem.width).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].chracterset").attr("value", elem.chracterset).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].fontsize").attr("value", elem.fontsize).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].fontcolor").attr("value", elem.fontcolor).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].bgcolor").attr("value", elem.bgcolor).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].bold_yn").attr("value", elem.bold_yn).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].italic_yn").attr("value", elem.italic_yn).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].underline_yn").attr("value", elem.underline_yn).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].between_text").attr("value", elem.between_text).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].between_line").attr("value", elem.between_line).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].vertical_align").attr("value", elem.vertical_align).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].horizontal_align").attr("value", elem.horizontal_align).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].zindex").attr("value", elem.zindex).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].font").attr("value", elem.font).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].resource_url").attr("value", elem.resource_url).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].resource_absoluteurl").attr("value", elem.resource_absoluteurl).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].org_height").attr("value", elem.org_height).appendTo("#frmSave");
            $("<input />").attr("type", "hidden").attr("name", "TemplateItemResources[" + idx + "].org_width").attr("value", elem.org_width).appendTo("#frmSave");

            idx++;
        });
        return true;
    });

    var container = [];
    var cropper;

    if ($("#Order_Code").val() == "") {
        alert("유효하지 않은 주문입니다.")
        location.replace("/");
    }

    invitation.initMode();
    var prodCateCode = $("#Product_Category_Code").val();

    if ($("#Photo_YN").val().toLowerCase() == "true") {
        $('.tdphoto').css('display', '')
    }
    else {
        $('.tdphoto').css('display', 'none')
    }

    $('#wrap').css('background-color', $("#Background_Color").val());
    if ($("#Objects").text() != "") {
        objects = JSON.parse($("#Objects").text());
        invitation.setItem();
    }
    invitation.setArea(JSON.parse($("#TB_Area").text()));

    if ($("#ETCs").text() != "") {
        etcs = JSON.parse($("#ETCs").text());
        etcs.forEach(function (elem) {
            $("#etc_title_" + elem.Sort).html(elem.Etc_Title);
            $("#etc_contents_" + elem.Sort).html(elem.Information_Content.replace(/\n/g, '<br/>'));
            $("#etc_title_" + elem.Sort).parents('li').css("padding-bottom", "10px");
        });
    }

    //혼주정보
    if ($("#Parents_Information_Use_YN").val() == "N") {
        $("#area4 .list_wrap").hide();
    } else {
        $("#area4 .list_wrap").show();
    }
    //갤러리
    if ($("#Gallery_Use_YN").val() == "N") {
        $("#area5").hide();
        $("#area6").hide();
    } else {
        $("#area5").show();
        $("#area6").show();
    }

    //동영상
    if ($("#Invitation_Video_Use_YN").val() == "N") {
        $("#area7").hide();
        $("#area8").hide();
    } else {
        $("#area7").show();
        $("#area8").show();
    }

    //기타정보
    if ($("#Etc_Information_Use_YN").val() == "N") {
        $("#area11").hide();
    } else {
        $("#area11").show();
    } 

    var accounts = [];
    if ($("#Accounts").text() != "") {
        accounts = JSON.parse($("#Accounts").text());
    }
    var groom_accounts = [];
    if ($("#GroomAccounts").text() != "") {
        groom_accounts = JSON.parse($("#GroomAccounts").text());
    }
    var bride_accounts = [];
    if ($("#BrideAccounts").text() != "") {
        bride_accounts = JSON.parse($("#BrideAccounts").text());
    }

    //축의금
    if ($("#MoneyGift_Remit_Use_YN").val() == "N" && $("#MoneyAccount_Remit_Use_YN").val() == "N" && $("#MoneyAccount_Div_Use_YN").val() == "N") {
        $("#area12").hide();
        $("#area13").hide();
    } else {

        //비회원
        if ($("#User_ID").val() == "") {
            $("#area12").show();
            $("#area13").show();

            //카카오
            $(".remittance_btn").hide();

            //통합
            if ($("#MoneyAccount_Remit_Use_YN").val() == "Y") {
                $(".an_btn.account").show();
            } else {
                $(".an_btn.account").hide();
            }

            //신랑측
            if ($("#MoneyAccount_Div_Use_YN").val() == "Y" && groom_accounts.length > 0) {
                $(".an_btn.groom").show();
            } else {
                $(".an_btn.groom").hide();
            }

            //신부측
            if ($("#MoneyAccount_Div_Use_YN").val() == "Y" && bride_accounts.length > 0) {
                $(".an_btn.bride").show();
            } else {
                $(".an_btn.bride").hide();
            }

        } else {
            $("#area12").show();
            $("#area13").show();

            //카카오
            if ($("#MoneyGift_Remit_Use_YN").val() == "Y") {
                $(".remittance_btn").show();
            } else {
                $(".remittance_btn").hide();
            }

            
            //통합
            if ($("#MoneyAccount_Remit_Use_YN").val() == "Y") {
                $(".an_btn.account").show();
            } else {
                $(".an_btn.account").hide();
            }

            //신랑측
            if ($("#MoneyAccount_Div_Use_YN").val() == "Y" && groom_accounts.length > 0) {
                $(".an_btn.groom").show();
            } else {
                $(".an_btn.groom").hide();
            }

            //신부측
            if ($("#MoneyAccount_Div_Use_YN").val() == "Y" && bride_accounts.length > 0) {
                $(".an_btn.bride").show();
            } else {
                $(".an_btn.bride").hide();
            }
        }
    }

    //방명록
    if ($("#GuestBook_Use_YN").val() == "N") {
        $("#area14").hide();
        $("#area15").hide();
    } else {
        $("#area14").show();
        $("#area15").show();
    }

    fonts.forEach(function (elem) {
        $("#selFont").append("<option value=\"" + elem.key + "\">" + elem.name + "</option>");
        $("#selFont option:eq(" + font_cnt + ")").css({ "font-family": elem.key });
        font_cnt++;
    });
    $("#selFont").on('change', function () {
        invitation.changeTextCss(this);
    });


    //매칭정보
    $("#selMatchInfo")
        .empty()
        .append("<option value=''>매칭정보추가</option>");
    var mappingfield = {};

    var mapfields = JSON.parse($("#MappingFields").text());

    for (var idx in mapfields) {
        var item = mapfields[idx];
        if (item.Mapping_YN == "Y" && item.Product_Category_Codes.includes(prodCateCode)) {
            var optionValue = "#" + item.ReserveWord + "#";
            mappingfield[optionValue] = { "name": item.ReserveWord };
            $("#selMatchInfo").append($("<option>", {
                value: optionValue,
                text: item.ReserveWord
            }));

            //input elm 의 match 수정
            $('#' + item.MappingField).attr('match', item.ReserveWord)
        }
    }

    //$('#selMatchInfo option').each(function () {
    //    if (this.value != "") {
    //        mappingfield[this.value] = { "name": this.text };
    //    }
    //});

    $("#Delete").on('click', function () {
        if ($(".item.selected").hasClass('photo-image')) {
            $('#btnphotoupload').addClass('btn-primary');
            $('#btnphotoupload').removeClass('btn-secondary');
            $('#btnphotoupload').addClass('empty');
            $('#btnphotoupload').text('추가');
        }
        invitation.pid = $(".item.selected").parent('div').attr('id');
        $(".item.selected").remove();
        $(".matchinfo.selected").remove();
        invitation.delObject();
        invitation.initMode();
        if ($("#" + invitation.pid + " .item").length < 1) {
            invitation.resizeArea('delete', invitation.pid);
        }
        invitation.showPlaceholder($('#' + invitation.pid));
    })
    $(document).on("keydown", function (event) {
        if (event.keyCode == 46) {

            if ($(".item.selected").hasClass('photo-image')) {
                $('#btnphotoupload').addClass('btn-primary');
                $('#btnphotoupload').removeClass('btn-secondary');
                $('#btnphotoupload').addClass('empty');
                $('#btnphotoupload').text('추가');
            }

            invitation.pid = $(".item.selected").parent('div').attr('id');
            $(".item.selected").remove();
            $(".matchinfo.selected").remove();
            invitation.delObject();
            invitation.initMode();
            if ($("#" + invitation.pid + " .item").length < 1) {
                invitation.resizeArea('delete', invitation.pid);
            }
            invitation.showPlaceholder($('#' + invitation.pid));
        }
    });
    $("input").on("focus", function (e) {
        $(document).unbind('keydown');

        var id = $(this).attr('indx');

        if (id != null) {
            var position = $("#area" + id).position().top;
            var scroll = $(".dvView>.card").scrollTop();
            var top = scroll + position;
            $(".dvView>.card").scrollTop(top);
        }
    });
    $("input").on("blur", function (e) {
        $(document).on("keydown", function (event) {
            if (event.keyCode == 46) {

                if ($(".item.selected").hasClass('photo-image')) {
                    $('#btnphotoupload').addClass('btn-primary');
                    $('#btnphotoupload').removeClass('btn-secondary');
                    $('#btnphotoupload').addClass('empty');
                    $('#btnphotoupload').text('추가');
                }

                invitation.pid = $(".item.selected").parent('div').attr('id');
                $(".item.selected").remove();
                $(".matchinfo.selected").remove();
                invitation.delObject();
                invitation.initMode();
                if ($("#" + invitation.pid + " .item").length < 1) {
                    invitation.resizeArea('delete', invitation.pid);
                }
                invitation.showPlaceholder($('#' + invitation.pid));
            }
        });
    });
    $("#Greetings",).on("focus", function (e) {
        $(document).unbind('keydown');

        var position = $("#area2").position().top;
        var scroll = $(".dvView>.card").scrollTop();
        var top = scroll + position;
        $(".dvView>.card").scrollTop(top);
    });
    $("#Greetings",).on("blur", function (e) {
        $(document).on("keydown", function (event) {
            if (event.keyCode == 46) {

                if ($(".item.selected").hasClass('photo-image')) {
                    $('#btnphotoupload').addClass('btn-primary');
                    $('#btnphotoupload').removeClass('btn-secondary');
                    $('#btnphotoupload').addClass('empty');
                    $('#btnphotoupload').text('추가');
                }

                invitation.pid = $(".item.selected").parent('div').attr('id');
                $(".item.selected").remove();
                $(".matchinfo.selected").remove();
                invitation.delObject();
                invitation.initMode();
                if ($("#" + invitation.pid + " .item").length < 1) {
                    invitation.resizeArea('delete', invitation.pid);
                }
                invitation.showPlaceholder($('#' + invitation.pid));


            }
        });
    });
    $('.invitationarea').mousedown(function (e) {

        $(".ui-resizable-handle").removeClass('resizabled');
        $(".resizable").removeClass("selected");
        $(".matchinfo").removeClass("selected");
        $(this).addClass("selected");
        area.id = $(this).attr('id');
        invitation.x = e.pageX - $(this).offset().left;
        invitation.y = e.pageY - $(this).offset().top;
        area.width = $(this).width();
        area.height = $(this).height();

        $(".ui-resizable-handle").hide();


        invitation.initMode();
        $("#divDisabled3").css("display", "none");
        $("#object_bgcolor").val($(this).attr("color_val"));
    });
    $("#selHour").find('option').each(function () {

        if ($(this).val() == $("#WeddingHour").val()) {
            $(this).attr("selected", "selected");
            $("#WeddingHour").val(this.value);
        }
    });
    $("#selMin").find('option').each(function () {
        if ($(this).val() == $("#WeddingMin").val()) {
            $(this).attr("selected", "selected");
            $("#WeddingMin").val(this.value);
        }
    });
    $('[match]').bind('change, keyup', function () {
        $('.matchinfo').trigger('keyup');

    })
    
    $("#close").click(function () {
        self.close();
    });

    $('.crop_btn').click(function () {
        $('.crop_btn').hide();
        var img_uri = cropper.getCroppedCanvas().toDataURL('image/jpeg').replace(/^data[:]image\/(png|jpg|jpeg)[;]base64,/i, "");

        fn_photoImageUpload(img_uri);
    });
    $("#selHour").change(function () {
        $("#selHour").find('option').each(function () {
            if ($(this).val() == $("#selHour option:selected").val()) {
                $(this).attr("selected", "selected");
                $("#WeddingHour").val(this.value);
                $("#WeddingHour").trigger("keyup");

            } else {
                $(this).attr("selected", false);
            }
        });
    });
    $("#selMin").change(function () {
        $("#selMin").find('option').each(function () {
            if ($(this).val() == $("#selMin option:selected").val()) {
                $(this).attr("selected", "selected");
                $("#WeddingMin").val(this.value);
                $("#WeddingMin").trigger("keyup");
            } else {
                $(this).attr("selected", false);
            }
        });
    });
    $("input[name='invitation_Detail.Time_Type_Code']:radio").change(function () {
        $("#Time_Type_Code").val(this.value);
        $("#Time_Type_Name").val(this.value);
        $("#Time_Type_Name").trigger("keyup");
    });
    $("input[name='invitation_Detail.Time_Type_Eng_YN']:radio").change(function () {
        $("#Time_Type_Eng_YN").val(this.value);

        if (this.value == "Y") {
            if ($("#Time_Type_Name").val() == "오후" || $("#Time_Type_Name").val() == "PM") {
                $("#Time_Type_Name").val("PM");
            } else {
                $("#Time_Type_Name").val("AM");
            }
        } else {
            if ($("#Time_Type_Name").val() == "오후" || $("#Time_Type_Name").val() == "PM") {
                $("#Time_Type_Name").val("오후");
            } else {
                $("#Time_Type_Name").val("오전");
            }
        }

        $("#Time_Type_Name").trigger("keyup");
    });
    $("input[name='invitation_Detail.WeddingWeek_Eng_YN']:radio").change(function () {
        $("#WeddingWeek_Eng_YN").val(this.value);

        $('#WeddingDate').trigger('change');
    });
    $('body').on('keyup', '.matchinfo', function () {
        invitation.changeText(this);
    });
    $('body').on('change', '.matchinfo.selected', function () {
        invitation.changeText(this);
    });
    $('body').on('change', '#FontSize', function () {
        invitation.changeTextCss(this);
    });
    $('body').on('keyup', '#FontSize', function () {
        invitation.changeTextCss(this);
    });
    $("#selMatchInfo").on('change', function () {
        $(".matchinfo.selected").val($(".matchinfo.selected").val() + this.value);
        $('.matchinfo').trigger('change');

        $("#selMatchInfo option:eq(0)").prop("selected", true);

        $(".matchinfo.selected").focus();
    });
    $('.btn-plus, .btn-minus').on('click', function (e) {
        const isNegative = $(e.target).closest('.btn-minus').is('.btn-minus');
        const input = $(e.target).closest('.inline-group').find('input');
        if (input.is('input')) {
            input[0][isNegative ? 'stepDown' : 'stepUp']()

            $('#FontSize').trigger('change');
        }
    })
    $('#Bold').on('click', function (e) {
        $(this).hasClass("selected") ? $(this).removeClass("selected") : $(this).addClass("selected");
        invitation.changeTextCss(this);
    });
    $('#Italic').on('click', function (e) {
        $(this).hasClass("selected") ? $(this).removeClass("selected") : $(this).addClass("selected");
        invitation.changeTextCss(this);
    });
    $('#Underline').on('click', function (e) {
        $(this).hasClass("selected") ? $(this).removeClass("selected") : $(this).addClass("selected");
        invitation.changeTextCss(this);
    });
    $('#Left').on('click', function (e) {
        $(this).addClass("selected");
        $('#Center').removeClass("selected");
        $('#Right').removeClass("selected");
        invitation.changeTextCss(this);
    });
    $('#Center').on('click', function (e) {
        $(this).addClass("selected");
        $('#Left').removeClass("selected");
        $('#Right').removeClass("selected");
        invitation.changeTextCss(this);
    });
    $('#Right').on('click', function (e) {
        $(this).addClass("selected");
        $('#Left').removeClass("selected");
        $('#Center').removeClass("selected");
        invitation.changeTextCss(this);
    });
    $('#Top').on('click', function (e) {
        $(this).addClass("selected");
        $('#Middle').removeClass("selected");
        $('#Bottom').removeClass("selected");
        invitation.changeTextCss(this);
    });
    $('#Middle').on('click', function (e) {
        $(this).addClass("selected");
        $('#Top').removeClass("selected");
        $('#Bottom').removeClass("selected");
        invitation.changeTextCss(this);
    });
    $('#Bottom').on('click', function (e) {
        $(this).addClass("selected");
        $('#Top').removeClass("selected");
        $('#Middle').removeClass("selected");
        invitation.changeTextCss(this);
    });
    $('#Between_Text_Calc').on('input change', function (e) {
        $("#Between_Text").val($(this).val())
        $("#Between_Text").trigger("blur");
        invitation.changeTextCss(this);
    });
    $('#Between_Line_Calc').on('input change', function (e) {
        $("#Between_Line").val($(this).val())
        $("#Between_Line").trigger("blur");
        invitation.changeTextCss(this);
    });
    $('body').on('keyup', '#Between_Text', function () {
        $("#Between_Text_Calc").val($(this).val())
        invitation.changeTextCss(this);
    });
    $('body').on('keyup', '#Between_Line', function () {
        $("#Between_Line_Calc").val($(this).val())
        invitation.changeTextCss(this);
    });
    $('#WeddingDate').change(function () {
        var d = new Date($('#WeddingDate').val());

        var week2;

        week = new Array('일', '월', '화', '수', '목', '금', '토', '일');

        if ($("#WeddingWeek_Eng_YN").val() == "Y") {
            week2 = new Array('SUN', 'MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT', 'SUN');
        } else {
            week2 = new Array('일', '월', '화', '수', '목', '금', '토', '일');
        }

        $('#WeddingYY').val(d.getFullYear());
        $('#WeddingDate').trigger('keyup');
        $('#WeddingMM').val(d.getMonth() + 1);
        $('#WeddingDD').val(d.getDate());
        $('#WeddingWeek').val(week[d.getDay()]);
        $('#WeddingWeekName').val(week2[d.getDay()]);
        $('#WeddingWeekName').trigger('keyup');
    });
    $('.addition_image_select').change(function () {
        if ($(this).val()) {
            var img_file = $(this).get(0).files;

            var img_URL = URL.createObjectURL(img_file[0]);

            var modal_con = '';

            modal_con += '<div class="crop_container" >';
            modal_con += '<img id="crop_preview" src="' + img_URL + "\" style=\" max-height: 400px;\">";
            modal_con += '</div>';

            var scale_x = 1;
            var scale_y = 1;

            if ($(window).width() > 750) {
                $('#call-cropImg').find('.modal-body').html(modal_con);

                var image = document.getElementById('crop_preview');


                var minWidth = $('#call-cropImg .modal-dialog').width() - 30;
                var minHeight = minWidth * scale_y;
                cropper = new Cropper(image, {
                    //aspectRatio: scale_x / scale_y,
                    minContainerWidth: minWidth,
                    minContainerHeight: minHeight,
                });

                $('#call-cropImg').modal('show');
            }
        }

    });
    $('#btnphotoupload').on('click', function () {

        if ($(this).hasClass('empty')) {
            $('#area1').trigger('mousedown');
            $('.crop_btn').show();
            $('.addition_image_select').val("");
            $('.addition_image_select').trigger('click');
        } else {
            $('#area1>.photo-image').remove();
            invitation.delObject();
            invitation.initMode();

            $('#btnphotoupload').addClass('btn-primary');
            $('#btnphotoupload').removeClass('btn-secondary');
            $('#btnphotoupload').addClass('empty');
            $('#btnphotoupload').text('추가');

        }
    });
    $('#btncssupload').on('click', function () {
        if ($(this).hasClass('empty')) {
            fn_addCSS();
        } else {
            fn_delCSS();
        }
    });
    $('#btnjsupload').on('click', function () {
        if ($(this).hasClass('empty')) {
            fn_addJS();
        } else {
            fn_delJS();
        }
    });
    $('#invitation_bgcolor').colorpicker({
        format: "hex"
    }).on('colorpickerChange', function (e) {
        var hexColor = "";
        if (e.color && e.color.isValid()) {
            hexColor = e.color.toHexString();
        }

        $('#wrap').css('background-color', hexColor);
    });

    $('#object_txtcolor').colorpicker({
        format: "hex"
    }).on('colorpickerChange', function (e) {

        var hexColor = "";
        if (e.color && e.color.isValid()) {
            hexColor = e.color.toHexString();
        }

        objects.forEach(function (elem) {
            if (elem.id == $(".item.selected").attr('id')) {
                elem.fontcolor = hexColor;
            }
        });
        $('.item.selected>.text').css('color', hexColor);
    });

    $('#object_bgcolor').colorpicker({
        format: "hex"
    }).on('colorpickerChange', function (e) {

        var hexColor = "";
        if (e.color && e.color.isValid()) {
            hexColor = e.color.toHexString();
        }

        if ($(".selected").attr('id').substring(0, 4) != 'area') {

            objects.forEach(function (elem) {
                if (elem.id == $(".item.selected").attr('id')) {
                    elem.bgcolor = hexColor;
                }
            });

            $('.item.selected>.text').css('background-color', hexColor);
            $('.item.selected').css('background-color', hexColor);

        } else {
            $('.invitationarea.selected').css('background-color', hexColor);
            $('.invitationarea.selected').attr("color_val", hexColor);
        }
    });
    $('.list_con').slideDown();
    $('.info_detail').on('click', function () {
        $(this).parent('.list_wrap').toggleClass('on');
        $('.list_con').slideToggle();
    });
    $('#gallery').find('figure').each(function () {
        var $link = $(this).find('a'),
            item = {
                src: $link.attr('href'),
                w: $link.data('width'),
                h: $link.data('height'),
                title: $link.data('caption')
            };
        container.push(item);
    });
    $('#gallery').find('figure').each(function () {
        var $link = $(this).find('a'),
            item = {
                src: $link.attr('href'),
                w: $link.data('width'),
                h: $link.data('height'),
                title: $link.data('caption')
            };
        container.push(item);
    });
    $('.remittance_btn.type01').click(function () {
        $('.remittance_pop_h').show();
    });
    $('.remittance_btn.type02').click(function () {
        $('.remittance_pop_w').show();
    });
    $('.close_btn').click(function () {
        $('.remittance_pop_h, .remittance_pop_w').hide();
    });
    $(".message_del").on('click', function () {
        $(this).parents('li').find(".password_check").slideToggle();
    });
    $("#chk_effect").on('click', function () {
        var chk = $(this).is(":checked")

        if (chk) {
            $("#area1").find(">div").not(".item").not(".ui-resizable-handle").not(".ui-resizable-s").each(function () {
                $(this).show();
            });

            if (typeof $(".d_day").val() != 'undefined') {
                $(".d_day").show();
            }

        } else {
            $("#area1").find(">div").not(".item").not(".ui-resizable-handle").not(".ui-resizable-s").each(function () {
                $(this).hide();
            });

            if (typeof $(".d_day").val() != 'undefined') {
                $(".d_day").hide();
            }
        }
    });
    $("#Weddinghall_Name").on("keyup", function () {
        $('#incNaverMap')[0].contentWindow.changeMarker();
    });
    $(".input_match").on("keyup", function () {
        var keyInput = $(this).attr("id");
        var inputValue = $(this).val();
        fn_match(keyInput, inputValue);
    });
    var galleryThumbs = new Swiper('.gallery-thumbs', {
        spaceBetween: 10,
        slidesPerView: 3,
        loop: true,
        freeMode: true,
        loopedSlides: 5,
        watchSlidesVisibility: true,
        watchSlidesProgress: true,
    });
    var galleryTop = new Swiper('.gallery-top', {
        spaceBetween: 10,
        loop: true,
        loopedSlides: 5,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        thumbs: {
            swiper: galleryThumbs,
        },
    });
    $('.invitationarea').mousedown(function (e) {
        $(".ui-resizable-handle").removeClass('resizabled');
        $(".resizable").removeClass("selected");
        $(this).addClass("selected");
        $(this).children(".ui-resizable-handle").addClass('resizabled');
        $(".ui-resizable-handle").hide();
        $(this).children(".ui-resizable-handle").show();
        $('.ui-resizable-handle.ui-resizable-s.resizabled').css("display", "block");
        $('.ui-resizable-handle.ui-resizable-s.resizabled').css('height', 0);
        $('.ui-resizable-handle.ui-resizable-s.resizabled').css('bottom', -2);

        e.stopPropagation();
    });
    $('.invitationarea').resizable({
        handles: 's'
    });
    $('.ui-resizable-handle.ui-resizable-s').css("display", "none");
    $(".guideline").css("height", $("#wrap").outerHeight())
    if ($("#Product_Category_Name").val() == "M감사장") {
        $("#area4 .list_wrap").hide();
    }
    if ($("#Attached_File1_URL").val() != "" && $("#Attached_File2_URL").val() != "") {
        $('#design_header span').show();
    }
    $.contextMenu({
        selector: '.invitationarea',
        trigger: 'right',
        callback: function (key, options, e) {
            if (e.which == 1) {
                invitation.initClass();

                if (key == "add_image") {
                    invitation.addImage();
                }
                else {
                    invitation.addText(key);
                }
            }
            else {
                return false;
            }
        },
        items: {
            "add_text": {
                name: "텍스트 추가",
                items: mappingfield
            },
            "add_image": { name: "이미지 추가" }
        }
    });
    $.contextMenu({
        selector: '.item',
        trigger: 'right',
        callback: function (key, options, e) {

            if (e.which == 1)


                switch (key) {
                    case "add_image":
                        invitation.initClass();
                        invitation.addImage();
                        break;
                    case "trash":
                        invitation.pid = $(".item.selected").parent('div').attr('id');
                        $(".item.selected").remove();
                        $(".matchinfo.selected").remove();
                        invitation.delObject();
                        invitation.initMode();
                        if ($("#" + invitation.pid + " .item").length < 1) {
                            invitation.resizeArea('delete', invitation.pid);
                        }
                        invitation.showPlaceholder($('#' + invitation.pid));
                        break;
                    case "bring_front":
                        invitation.bring_front();

                        break;
                    case "bring_forward":
                        invitation.bring_forward();
                        break;
                    case "send_back":
                        invitation.send_back();
                        break;
                    case "send_backward":
                        invitation.send_backward();
                        break;
                    default:
                        invitation.initClass();
                        invitation.addText(key);
                        break;
                }

        },
        items: {
            "add_text": {
                name: "텍스트 추가",
                items: mappingfield
            },
            "add_image": { name: "이미지 추가" },
            "sep1": "---------",
            "trash": { name: "삭제" },
            "sep2": "---------",
            "bring_front": {
                name: "맨 앞으로 가져오기",
                disabled: function () {

                    var pid = $(".item.selected").parent('div').attr('id');

                    return $("#" + pid + " > .item.selected").attr('id') == $("#" + pid + " > .item").last().attr('id') ? true : false;

                }
            },
            "bring_forward": {
                name: "앞으로 가져오기",
                disabled: function () {

                    var pid = $(".item.selected").parent('div').attr('id');

                    return $("#" + pid + " > .item.selected").attr('id') == $("#" + pid + " > .item").last().attr('id') ? true : false;

                }
            },
            "send_back": {
                name: "맨 뒤로 보내기",
                disabled: function () {


                    var pid = $(".item.selected").parent('div').attr('id');

                    return $("#" + pid + " > .item.selected").attr('id') == $("#" + pid + " > .item").first().attr('id') ? true : false;

                }
            },
            "send_backward": {
                name: "뒤로 보내기",
                disabled: function () {

                    var pid = $(".item.selected").parent('div').attr('id');
                    return $("#" + pid + " > .item.selected").attr('id') == $("#" + pid + " > .item").first().attr('id') ? true : false;

                }
            }
        }
    });

    loadGuestbook($("#Invitation_ID").val(), 'next', 0);

    $("#WeddingDate").on("keyup", function () {
        setDday($("#WeddingDate").val());
    });

    $(".invitation_open").on("click", function () {
        window.open($(".td_Invitation_URL strong").html(), "_blank");
    });

    $(".invitation_copy").on("click", function () {

        var url = $(".td_Invitation_URL").find('strong').text();

        copyToClipboardByText(url);
    });


    if ($("#Invitation_Video_Use_YN").val() == "Y") {
        setVideo($("#Invitation_Video_Type_Code").val());
    }

    if ($("#Product_Category_Code").val() == "PCC02") {
        //감사장
        $("#area4 .list_wrap").hide();
        $("#area5").hide();
        $("#area6").hide();
        $("#area7").hide();
        $("#area8").hide();
        $("#area9").hide();
        $("#area10").hide();
        $("#area11").hide();
        $("#area12").hide();
        $("#area13").hide();
        $("#area14").hide();
        $("#area15").hide();
    }

    //파일 업로드 실행
    $("#uploadFrm #file").on("change", function (e) {
        $("#uploadFrm").submit();
        
    });
    $("body").on("submit", '#uploadFrm', function (e) {
        e.preventDefault();
        $(".loader").show();

        var form = $("#uploadFrm");
        var formData = new FormData(form[0]);
        $.ajax({
            url: form.attr('action'),
            data: formData,
            type: form.attr('method'),
            enctype: form.attr('enctype'),
            processData: false,
            contentType: false,
            dataType: 'json',
            cache: false
        }).done(function (result) {
            if (result.success) {
                if (result.typeCode == "Image") {

                    invitation.org_width = result.width;
                    invitation.org_height = result.height

                    if (result.width >= area.width) {
                        invitation.x = 0;
                        invitation.y = 0;

                        result.height = parseFloat(result.height * area.width / result.width)

                        result.width = parseFloat(area.width)

                        if (area.height < result.height) {
                            $('#' + area.id).height(result.height);
                            area.height = result.height;
                        }
                    } else {
                        if (result.height >= area.height) {
                            $('#' + area.id).height(result.height);
                            area.height = result.height;
                            invitation.y = 0;
                        }
                    }

                    invitation.resource_url = result.resource_url;
                    invitation.resource_absoluteurl = result.resource_absoluteurl;
                    invitation.width = result.width;
                    invitation.height = result.height

                    invitation.setInvitationIdx();

                    invitation.id = 'item_' + invitation.idx;
                    var div = "<div id='" + invitation.id + "' idx='" + invitation.idx + "' class='item ui-widget-content selected resizable' style='top: " + invitation.y + "px; left: " + invitation.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + invitation.resource_absoluteurl + "' width='" + invitation.width + "px' height='" + invitation.height + "px'  /><span class='img-info'></span></div>";
                    $('#' + area.id).append(div);
                    invitation.addObject($(this), 'img');
                    invitation.imageMode();
                    invitation.addImageEvent();
                    invitation.hidePlaceholder($('#' + area.id));

                } else if (result.typeCode == "photo") {
                    invitation.org_width = result.width;
                    invitation.org_height = result.height

                    if (result.width >= area.width) {
                        invitation.x = 0;
                        invitation.y = 0;

                        result.height = parseFloat(result.height * area.width / result.width)

                        result.width = parseFloat(area.width)

                        if (area.height < result.height) {
                            $('#area1').height(result.height);
                            area.height = result.height;
                        }
                    }

                    invitation.resource_url = result.resource_url;
                    invitation.resource_absoluteurl = result.resource_absoluteurl;
                    invitation.width = result.width;
                    invitation.height = result.height;

                    $("#Delegate_Image_URL").val(result.resource_url);
                    $("#Delegate_Image_AbsoluteUri").val(result.resource_absoluteurl);
                    $("#Delegate_Image_Height").val(result.width);
                    $("#Delegate_Image_Width").val(result.height);

                    invitation.setInvitationIdx();

                    invitation.id = 'item_' + invitation.idx;
                    var div = "<div id='" + invitation.id + "' idx='" + invitation.idx + "' class='item ui-widget-content selected resizable photo-image' style='top: " + invitation.y + "px; left: " + invitation.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + invitation.resource_absoluteurl + "' width='" + invitation.width + "px' height='" + invitation.height + "px'  /><span class='img-info'></span></div>";
                    $('#area1').append(div);
                    $('#area1').removeClass('selected');


                    $('#btnphotoupload').removeClass('btn-primary');
                    $('#btnphotoupload').addClass('btn-secondary');
                    $('#btnphotoupload').removeClass('empty');
                    $('#btnphotoupload').text('삭제');

                    invitation.addObject($(this), 'photo');
                    invitation.imageMode();
                    invitation.addImageEvent();

                    $('.close[data-dismiss="modal"]').trigger('click');
                }

            } else {
                alert(result.message);
            }
            $(".loader").hide();
        });
    });

    $(".input-daterange").each(function () {
        var $inputs = $(this).find('input:text');
        $inputs.datepicker({
            showOn: "both", //focus, button, both 중에 선택할 수 있습니다.focus 는 포커스가 오면 달력이 팝업 됩니다.button 은 버튼을 클릭하면 달력이 팝업 됩니다.both는 두 가지 경우 모두에서 팝업 됩니다.
            todayBtn: "linked",
            autoclose: true,
            language: "ko",
            format: "yyyy-mm-dd"
        });

        if ($inputs.length >= 2) {
            var $from = $inputs.eq(0);
            var $to = $inputs.eq(1);

            $from.on('changeDate', function (e) {
                var d = new Date(e.date.valueOf());
                $to.datepicker('setStartDate', d); // 종료일은 시작일보다 빠를 수 없다.
            });
            $to.on('changeDate', function (e) {

                var d = new Date(e.date.valueOf());
                $from.datepicker('setEndDate', d); // 시작일은 종료일보다 늦을 수 없다.
            });
        }
    })
});

function copyToClipboard(elementId) {
    var aux = document.createElement("input");
    aux.setAttribute("value", document.getElementById(elementId).innerHTML);
    document.body.appendChild(aux);
    aux.select();
    document.execCommand("copy");
    document.body.removeChild(aux);
}

function copyToClipboardByText(text) {
    var aux = document.createElement("textarea");
    aux.value = text;
    document.body.appendChild(aux);
    aux.select();
    aux.setSelectionRange(0, 9999);
    document.execCommand("copy");
    document.body.removeChild(aux);
}

function validation_check() {
    if ($('.tdphoto').css('display') != 'none' && $("#btnphotoupload").hasClass('empty')) {
        alert("포토이미지를 추가하세요.");
        return false;
    }

    return true;
}

function dataURLtoFile(dataurl, filename) {

    var arr = dataurl.split(','),
        mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]),
        n = bstr.length,
        u8arr = new Uint8Array(n);

    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, { type: mime });
}

function fn_photoImageUpload(img_uri) {
    tempFileUpload('photo', img_data);
}

function fn_replace(str, searchStr, replaceStr) {
    return str.split(searchStr).join(replaceStr);
}
function fn_match(target, txt) {
    $("#" + target).val(txt);
    if ($("span.i_" + target).length > 0) {
        $("span.i_" + target).text(txt);
    }
    $('.matchinfo').trigger('keyup');
}

function loadGuestbook(id) {
    var url = $("#GuestbookUrl").val() + '&Id=' + id;
    $.ajax({
        url: url,
        method: 'get',
        data: {},
        success: function (html) {
            if (id == 0) {
                $('#guestbook').html(html);
            }
            else {
                $('#guestbook').append(html);
            }
        }
    });
}

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function setDday(wedding_date) {
    if (typeof $(".d_day").val() != 'undefined') {
        var today = new Date();
        var wedd_day = new Date(wedding_date);
        var distance = wedd_day - today
        var dday = Math.ceil(distance / (1000 * 60 * 60 * 24));
        if (parseInt(dday) > 0) {
            //예식일이전
            $(".d_day").text("D-" + parseInt(dday));
            $(".d_day").show();
        } else if (parseInt(dday) == 0) {
            $(".d_day").text("D-Day");
            $(".d_day").show();
            //예식일당일
        } else {
            //예식일이후
            $(".d_day").hide();
        }
    }
}

function setVideo(id) {
    switch (id) {
        case "VTC01"://Youtube
            var htmlInput = $("#Invitation_Video_URL").val();
            var pattern = /(?:http?s?:\/\/)?(?:www\.)?(?:youtube\.com|youtu\.be)\/(?:watch\?v=)?(.+)/g;

            if (pattern.test(htmlInput)) {
                var replacement = '<iframe src="//www.youtube.com/embed/$1" frameborder="0" class="embed-container" allowfullscreen></iframe>';


                htmlInput = htmlInput.replace(pattern, replacement);
            }

            $(".iframe_wrap").html(htmlInput);

            break;
        case "VTC02": //Vimeo
            $(".iframe_wrap").html($("#Invitation_Video_URL").val());
            break;
        case "VTC03"://FEELMAKER
            $(".iframe_wrap").html($("#Invitation_Video_URL").val());
            break;

    }

}

$(window).on('load', function () {
    if (typeof $(".d_day").val() != 'undefined') {
        $(".d_day").css("font-size", parseInt($(".d_day").css("font-size").replace("px", ""))  + "px");
        $(".d_day").show();
    }
    setDday($("#WeddingDate").val());
    //$("#wrap").find(">div").show();
    $("#wrap").find("#area3").hide();
    $(".loader_preview").hide();
});

//파일 업로드 팝업

function tempFileUpload(typeCode, img_data) {
    $("#uploadFrm")[0].reset();
    $("#uploadFrm input[name=TypeCode]").val(typeCode);
    if (typeCode == 'photo') {
        $("#uploadFrm input[name=imageData]").val(img_data);
        $("#uploadFrm").submit();
    } else {
        $("#uploadFrm #file").click();
    }
}









