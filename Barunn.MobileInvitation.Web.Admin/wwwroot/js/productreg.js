var container = [];
var cropper;

$(document).ready(function () {

    /* Product Category - Start */
    $("#tc li").remove();
    var prodCates = JSON.parse($("#JsonProductCategories").text());
    prodCates.forEach(function (cate) {
        $("#tc").append("<li id='" + cate.Code + "'>" + cate.Name + "</li>");
    });
    ProdCateRenderSelected();

    //category 선택
    $('#tc li').each(function (index, item) {
        $(item).click(function (e) {

            $('#tc li').removeClass("selected");
            $(this).addClass("selected");
            $("#mc").empty();

            var code = $(this).attr('id');
            var prodCates = JSON.parse($("#JsonProductCategories").text());
            var objidx = prodCates.findIndex(v => v.Code == code);

            var childs = prodCates[objidx].Childs;
            if (childs) {
                childs.forEach(function (cate) {
                    var li = "<li id='" + cate.Code + "'>" + cate.Name + "</li>"
                    $("#mc").append(li);
                });
                $('#mc li').each(function (index, item) {
                    $(item).click(function (e) {
                        $('#mc li').removeClass("selected");
                        $(this).addClass("selected");
                    });
                });
            }
            
        });
    });
    /* Product Category - End */

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

    /* Template area - Start */

    template.initMode();
    template.setArea(JSON.parse($("#TB_Area").text()));

    $("#Photo_YN").change();

    $('#wrap').css('background-color', $("#TemplateModel_Template_Background_Color").val());
    if ($("#Objects").text() != "") {
        objects = JSON.parse($("#Objects").text());
        template.setItem();
    }
    if ($("#Template_ID").val() == 0) {
        template.getTemplateList();
        template.copyTitle();
    }

    fonts.forEach(function (elem) {
        $("#selFont").append("<option value=\"" + elem.key + "\">" + elem.name + "</option>");
        $("#selFont option:eq(" + font_cnt + ")").css({ "font-family": elem.key });
        font_cnt++;
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

    $('#template_bgcolor').colorpicker({
        autoInputFallback: false,
        autoHexInputFallback: false,
        format: "hex"
    }).on('colorpickerChange', function (e) {
        if (e.color && e.color.isValid()) {
            var hexColor = e.color.toHexString();
            $('#wrap').css('background-color', hexColor);

        } else if (e.color == null) {
            $('#wrap').css('background-color', '');

        }
    });
    $('#object_txtcolor').colorpicker({
        autoInputFallback: false,
        autoHexInputFallback: false,
        format: "hex"
    }).on('colorpickerChange', function (e) {
        if (e.color && e.color.isValid()) {
            var hexColor = e.color.toHexString();

            objects.forEach(function (elem) {
                if (elem.id == $(".item.selected").attr('id')) {
                    elem.fontcolor = hexColor;
                }
            });
            $('.item.selected>.text').css('color', hexColor);
        } else if (e.color == null) {
            objects.forEach(function (elem) {
                if (elem.id == $(".item.selected").attr('id')) {
                    elem.fontcolor = '';
                }
            });
            $('.item.selected>.text').css('color', '');
        }
    });
    $('#object_bgcolor').colorpicker({
        autoInputFallback: false,
        autoHexInputFallback: false,
        format: "hex"
    }).on('colorpickerChange', function (e) {

        if (e.color && e.color.isValid()) {
            var hexColor = e.color.toHexString();

            if ($(".selected").attr('id').substring(0, 4) != 'area') {

                objects.forEach(function (elem) {
                    if (elem.id == $(".item.selected").attr('id')) {
                        elem.bgcolor = hexColor;
                    }
                });

                $('.item.selected>.text').css('background-color', hexColor);
                $('.item.selected').css('background-color', hexColor);

            } else {
                $('.templatearea.selected').css('background-color', hexColor);
                $('.templatearea.selected').attr("color_val", hexColor);
            }
        } else if (e.color == null) {
            if ($(".selected").attr('id').substring(0, 4) != 'area') {

                objects.forEach(function (elem) {
                    if (elem.id == $(".item.selected").attr('id')) {
                        elem.bgcolor = '';
                    }
                });

                $('.item.selected>.text').css('background-color', '');
                $('.item.selected').css('background-color', '');

            } else {
                $('.templatearea.selected').css('background-color', '');
                $('.templatearea.selected').attr("color_val", '');
            }
        }
    });
    $('.list_con').slideDown();
    
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
    
    $('.ui-resizable-handle.ui-resizable-s').css("display", "none");
    if ($("#Product_Category_Name").val() == "M감사장") {
        $("#area4 .list_wrap").hide();
    }

    fn_repalce_btncssupload();
    fn_repalce_btnjsupload();

    $(".loader").hide();
});

$(".dvAdd > a").click(function (e) {
    var code = "";
    if ($("#tc .selected").length > 0) {

        code = $("#tc .selected").attr('id');

        if ($("#mc .selected").length > 0) {
            code = $("#mc .selected").attr('id');
        }

        $("select[name=SelectedProductCategories] option").map(function (i, el) {
            if ($(el).val() == code) {
                $(el).prop("selected", true);
            }
        })
        ProdCateRenderSelected();
    }
});

//Product brand change
$(".selectProdBrand").on('change', function () {

    var p = $(this).find(":selected").val();

    var mydata = {
        "productBarnd": p
    };
    $.ajax({
        type: "POST",
        dataType: 'json',
        data: mydata,
        url: $("#aGetNewProductCode").attr("href"),
        success: function (result) {
            $('#Original_Product_Code').val(result.code);
        }
    });
});
//Product Category change
$("input:radio[id=ProductCategoryCode]").on('change', function () {
    template.setArea(JSON.parse($("#TB_Area").text()));
    if ($("#Template_ID").val() == 0) {
        template.getTemplateList();
    }
    initTemplateLayer();
});
//Photo_YN change
$("#Photo_YN").on('change', function () {
    if ($("#Photo_YN").is(':checked')) {
        $('.tdphoto').css('display', '')
    }
    else {
        $('.tdphoto').css('display', 'none')
    }
    template.setArea(JSON.parse($("#TB_Area").text()));
    if ($("#Template_ID").val() == 0) {
        template.getTemplateList();
    }
    initTemplateLayer();
});
//제품이미지 추가
$("a.addProductImage").on('click', function (e) {
    e.preventDefault();
    var categoryCode = $(this).prev().val();
    fileUpload(categoryCode, 'product', '');
});

//제품이미지 삭제
$("a.removeProductImage").on('click', function (e) {
    e.preventDefault();
    let categoryCode = $(this).prev().prev().val();
    let codeImageListElements = $('ul.' + categoryCode);
    codeImageListElements.find('input[type=checkbox]:checked').each(function (index, item) {
        $(item).parent().remove();
    });
});

//메인 이미지 등록/변경
$("a.addMainImage").on('click', function (e) {
    e.preventDefault();
    fileUpload('main', 'product', '');
});

//미리보기 이미지 등록/변경
$("a.addPreImage").on('click', function (e) {
    e.preventDefault();
    fileUpload('pre', 'product', '');
});

//파일 업로드 팝업
function fileUpload(typeCode, root, leaf) {
    $("#frmFileUpload")[0].reset();
    $("#frmFileUpload input[name=TypeCode]").val(typeCode);
    $("#frmFileUpload input[name=RootPath]").val(root);
    $("#frmFileUpload input[name=LeafPath]").val(leaf);
    $("#frmFileUpload #file").click();
}

//파일 업로드 실행
$("#frmFileUpload #file").on("change", function (e) {
    $(".loader").show();

    var form = $("#frmFileUpload");
    var formData = new FormData(form[0]);
    $.ajax({
        url: form.attr('action'),
        data: formData,
        type: form.attr('method'),
        enctype: form.attr('enctype'),
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.status) {
            if (result.typeCode == "main") {
                $("#imgMainImageUri").attr("src", result.files[0].imageAbsoluteUri);
                $("input[type=hidden][name=Main_ImageAbsoluteUri]").val(result.files[0].imageAbsoluteUri);
                $("input[type=hidden][name=Main_Image_URL]").val(result.files[0].image_URL);
            } else if (result.typeCode == "pre") {
                $("#imgPreImageUri").attr("src", result.files[0].imageAbsoluteUri);
                $("input[type=hidden][name=Preview_ImageAbsoluteUri]").val(result.files[0].imageAbsoluteUri);
                $("input[type=hidden][name=Preview_Image_URL]").val(result.files[0].image_URL);
            } else {
                var codeImageListElements = $('ul.' + result.typeCode);
                $(result.files).each(function (index, item) {
                    var idx = newGuid();
                    codeImageListElements.append('<li class="pcImageItem"><input type="checkbox" />' +
                        '<input type="hidden" name="ProductImages.index" value="' + idx + '" />' +
                        '<img src="' + item.imageAbsoluteUri + '" class="pcImage mr-2" />' +
                        '<input type="hidden" name="ProductImages[' + idx + '].Image_ID" value="' + item.image_ID + '" />' +
                        '<input type="hidden" name="ProductImages[' + idx + '].Image_URL" value="' + item.image_URL + '" />' +
                        '<input type="hidden" name="ProductImages[' + idx + '].Image_Type_Code" value="' + item.image_Type_Code + '" />' +
                        '<input type="hidden" name="ProductImages[' + idx + '].ImageAbsoluteUri" value="' + item.imageAbsoluteUri + '" />' +
                        '<input type="hidden" name="ProductImages[' + idx + '].UpdateDateTime" value="' + item.updateDateTime + '" />' +
                        '</li>');
                });
            }

        } else {
            alert(result.message);
        }
        $(".loader").hide();
    });
});

//Template 팝업
$("#aTemplete").on("click", function (e) {
    e.preventDefault();

    $("#divtemplate").modal("show");
    $(".guideline").css("height", $("#wrap").outerHeight())

    fn_repalce_btncssupload();
    fn_repalce_btnjsupload();
});
//Template 팝업 닫기 - 이미지 생성
$("#btnTemplateClose").on("click", function (e) {
    e.preventDefault();
    template.initClass();
    //Main Image 생성
    html2canvas($("#area1").get(0), {
        logging: true,
        allowTaint: true,
        useCORS: true
    }).then(function (canvas) {
        var imageData = canvas.toDataURL('image/jpeg').replace(/^data[:]image\/(png|jpg|jpeg)[;]base64,/i, "");
        tempFileUpload('mainimage', imageData);
    });

    $("#divtemplate").modal("hide");
    fn_repalce_btncssupload();
    fn_repalce_btnjsupload();
});
//Template 팝업 닫기 - 이미지 생성 않함.
$("#btntemplateclose").on("click", function (e) {
    e.preventDefault();
    $("#divtemplate").modal("hide");
    fn_repalce_btncssupload();
    fn_repalce_btnjsupload();
});

var keyStop = {
    8: ":not(input:text, textarea, input:file, input:password)", // stop backspace = back
    13: "input:text, input:password", // stop enter = submit 
    end: null
};
$("#frmSave").keydown(function (event) {
    var selector = keyStop[event.which];
    if (selector !== undefined && $(event.target).is(selector)) {
        event.preventDefault(); //stop event
    }
});

//Form submit
$("#frmSave").submit(function (e) {
    $("#divtemplate").modal("show");

    //Main Image 생성 및 업로드

    var idx = 0;
    //Add Area data: TemplateModel.TemplateAreas.~
    tb_area.forEach(function (elem) {
        $("div[id^=area]").each(function () {
            if ($(this).attr('idx') == elem.Area_ID) {
                if ($(this).hasClass('templatearea')) {
                    $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateAreas[" + idx + "].Area_ID").attr("value", elem.Area_ID).appendTo("#frmSave");
                    $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateAreas[" + idx + "].Size_Height").attr("value", $(this).height()).appendTo("#frmSave");
                    $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateAreas[" + idx + "].Size_Width").attr("value", $(this).width()).appendTo("#frmSave");
                    $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateAreas[" + idx + "].Color").attr("value", $(this).attr("color_val")).appendTo("#frmSave");
                    $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateAreas[" + idx + "].Sort").attr("value", elem.Area_ID).appendTo("#frmSave");

                    idx++;
                }
            }
        });
    });

    //Add Item Resources TemplateModel.TemplateItemResources.~
    template.initObject(); //먼저 .item div의 순서대로 objects 재작성.
    idx = 0;
    objects.forEach(function (elem) {
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].item_id").attr("value", elem.item_id).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].resource_id").attr("value", elem.resource_id).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].pid").attr("value", elem.pid).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].id").attr("value", elem.id).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].type").attr("value", elem.type).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].top").attr("value", elem.top).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].left").attr("value", elem.left).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].height").attr("value", elem.height).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].width").attr("value", elem.width).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].chracterset").attr("value", elem.chracterset).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].fontsize").attr("value", elem.fontsize).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].fontcolor").attr("value", elem.fontcolor).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].bgcolor").attr("value", elem.bgcolor).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].bold_yn").attr("value", elem.bold_yn).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].italic_yn").attr("value", elem.italic_yn).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].underline_yn").attr("value", elem.underline_yn).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].between_text").attr("value", elem.between_text).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].between_line").attr("value", elem.between_line).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].vertical_align").attr("value", elem.vertical_align).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].horizontal_align").attr("value", elem.horizontal_align).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].zindex").attr("value", elem.zindex).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].font").attr("value", elem.font).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].resource_url").attr("value", elem.resource_url).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].resource_absoluteurl").attr("value", elem.resource_absoluteurl).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].org_height").attr("value", elem.org_height).appendTo("#frmSave");
        $("<input />").attr("type", "hidden").attr("name", "TemplateModel.TemplateItemResources[" + idx + "].org_width").attr("value", elem.org_width).appendTo("#frmSave");

        idx++;
    });
    $("#divtemplate").modal("hide");
    return true;
});

/* Product area - End */

/* Template area - Start */


$("#selFont").on('change', function () {
    template.changeTextCss(this);
});


$("#Delete").on('click', function () {
    setActivePhotoButton();

    template.pid = $(".item.selected").parent('div').attr('id');
    $(".item.selected").remove();
    $(".matchinfo.selected").remove();
    template.delObject();
    template.initMode();
    if ($("#" + template.pid + " .item").length < 1) {
        template.resizeArea('delete', template.pid);
    }
    template.showPlaceholder($('#' + template.pid));
})

$(document).on("keydown", function (event) {
    if (event.keyCode == 46) {

        setActivePhotoButton();

        template.pid = $(".item.selected").parent('div').attr('id');
        $(".item.selected").remove();
        $(".matchinfo.selected").remove();
        template.delObject();
        template.initMode();
        if ($("#" + template.pid + " .item").length < 1) {
            template.resizeArea('delete', template.pid);
        }
        template.showPlaceholder($('#' + template.pid));


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

            setActivePhotoButton();

            template.pid = $(".item.selected").parent('div').attr('id');
            $(".item.selected").remove();
            $(".matchinfo.selected").remove();
            template.delObject();
            template.initMode();
            if ($("#" + template.pid + " .item").length < 1) {
                template.resizeArea('delete', template.pid);
            }
            template.showPlaceholder($('#' + template.pid));


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

            setActivePhotoButton();

            template.pid = $(".item.selected").parent('div').attr('id');
            $(".item.selected").remove();
            $(".matchinfo.selected").remove();
            template.delObject();
            template.initMode();
            if ($("#" + template.pid + " .item").length < 1) {
                template.resizeArea('delete', template.pid);
            }
            template.showPlaceholder($('#' + template.pid));


        }
    });
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
    croptype = $("input[name='cropTypeCode']").val();
    fn_cropImageUpload(croptype,img_uri);
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

$("input[name='TemplateModel.TemplateDetail.Time_Type_Code']:radio").change(function () {
    $("#Time_Type_Code").val(this.value);
    $("#Time_Type_Name").val(this.value);
    $("#Time_Type_Name").trigger("keyup");
});
$("input[name='TemplateModel.TemplateDetail.Time_Type_Eng_YN']:radio").change(function () {
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
$("input[name='TemplateModel.TemplateDetail.WeddingWeek_Eng_YN']:radio").change(function () {
    $("#WeddingWeek_Eng_YN").val(this.value);

    $('#WeddingDate').trigger('change');
});
$('body').on('keyup', '.matchinfo', function () {
    template.changeText(this);
});
$('body').on('change', '.matchinfo.selected', function () {
    template.changeText(this);
});
$('body').on('change', '#FontSize', function () {
    template.changeTextCss(this);
});
$('body').on('keyup', '#FontSize', function () {
    template.changeTextCss(this);
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
    template.changeTextCss(this);
});
$('#Italic').on('click', function (e) {
    $(this).hasClass("selected") ? $(this).removeClass("selected") : $(this).addClass("selected");
    template.changeTextCss(this);
});
$('#Underline').on('click', function (e) {
    $(this).hasClass("selected") ? $(this).removeClass("selected") : $(this).addClass("selected");
    template.changeTextCss(this);
});
$('#Left').on('click', function (e) {
    $(this).addClass("selected");
    $('#Center').removeClass("selected");
    $('#Right').removeClass("selected");
    template.changeTextCss(this);
});
$('#Center').on('click', function (e) {
    $(this).addClass("selected");
    $('#Left').removeClass("selected");
    $('#Right').removeClass("selected");
    template.changeTextCss(this);
});
$('#Right').on('click', function (e) {
    $(this).addClass("selected");
    $('#Left').removeClass("selected");
    $('#Center').removeClass("selected");
    template.changeTextCss(this);
});
$('#Top').on('click', function (e) {
    $(this).addClass("selected");
    $('#Middle').removeClass("selected");
    $('#Bottom').removeClass("selected");
    template.changeTextCss(this);
});
$('#Middle').on('click', function (e) {
    $(this).addClass("selected");
    $('#Top').removeClass("selected");
    $('#Bottom').removeClass("selected");
    template.changeTextCss(this);
});
$('#Bottom').on('click', function (e) {
    $(this).addClass("selected");
    $('#Top').removeClass("selected");
    $('#Middle').removeClass("selected");
    template.changeTextCss(this);
});
$('#Between_Text_Calc').on('input change', function (e) {
    $("#Between_Text").val($(this).val())
    $("#Between_Text").trigger("blur");
    template.changeTextCss(this);
});
$('#Between_Line_Calc').on('input change', function (e) {
    $("#Between_Line").val($(this).val())
    $("#Between_Line").trigger("blur");
    template.changeTextCss(this);
});
$('body').on('keyup', '#Between_Text', function () {
    $("#Between_Text_Calc").val($(this).val())
    template.changeTextCss(this);
});
$('body').on('keyup', '#Between_Line', function () {
    $("#Between_Line_Calc").val($(this).val())
    template.changeTextCss(this);
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
        $("input[name='cropTypeCode']").val('photo');
        $('.crop_btn').show();
        $('.addition_image_select').val("");
        $('.addition_image_select').trigger('click');

    } else {
        $("input[name='cropTypeCode']").val('');
        $('#area1>.photo-image').remove();
        template.delObject();
        template.initMode();

        $('#btnphotoupload').addClass('btn-primary');
        $('#btnphotoupload').removeClass('btn-secondary');
        $('#btnphotoupload').addClass('empty');
        $('#btnphotoupload').text('추가');

    }
});

$('#btnprofileupload').on('click', function () {

    if ($(this).hasClass('empty')) {
        $('#area18').trigger('mousedown');
        $("input[name='cropTypeCode']").val('profile');
        $('.crop_btn').show();
        $('.addition_image_select').val("");
        $('.addition_image_select').trigger('click');

    } else {
        $("input[name='cropTypeCode']").val('');
        $('#area18>.profile-image').remove();
        template.delObject();
        template.initMode();

        $('#btnprofileupload').addClass('btn-primary');
        $('#btnprofileupload').removeClass('btn-secondary');
        $('#btnprofileupload').addClass('empty');
        $('#btnprofileupload').text('추가');

    }
});

//Photo 팝업 닫기
$("#btncall-cropImgClose").on("click", function (e) {
    e.preventDefault();
    $("#call-cropImg").modal("hide");
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

$('.info_detail').on('click', function () {
    $(this).parent('.list_wrap').toggleClass('on');
    $('.list_con').slideToggle();
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

$(document).on('mousedown', '.templatearea', function (e) {
    $(".ui-resizable-handle").removeClass('resizabled');
    $(".resizable").removeClass("selected");
    $(".matchinfo").removeClass("selected");

    $(this).addClass("selected");
    $(this).children(".ui-resizable-handle").addClass('resizabled');

    area.id = $(this).attr('id');
    template.x = e.pageX - $(this).offset().left;
    template.y = e.pageY - $(this).offset().top;
    area.width = $(this).width();
    area.height = $(this).height();

    $(".ui-resizable-handle").hide();
    $(this).children(".ui-resizable-handle").show();

    $('.ui-resizable-handle.ui-resizable-s.resizabled').css("display", "block");
    $('.ui-resizable-handle.ui-resizable-s.resizabled').css('height', 0);
    $('.ui-resizable-handle.ui-resizable-s.resizabled').css('bottom', -2);

    template.initMode();
    $("#divDisabled3").css("display", "none");
    $("#object_bgcolor").val($(this).attr("color_val"));

    e.stopPropagation();
});

$('.templatearea').resizable({
    handles: 's'
});
//파일 업로드 실행
$("#uploadFrm #file").on("change", function (e) {
    $("#uploadFrm").submit();
});
/* Template area - End */

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
            if (result.typeCode == "templateImage") {
                template.org_width = result.width;
                template.org_height = result.height;
                if (result.width >= area.width) {
                    template.x = 0;
                    template.y = 0;

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
                        template.y = 0;
                    }
                }

                template.resource_url = result.resource_url;
                template.resource_absoluteurl = result.resource_absoluteurl;
                template.width = result.width;
                template.height = result.height

                template.setTemplateIdx();

                template.id = 'item_' + template.idx;
                var div = "<div id='" + template.id + "' idx='" + template.idx + "' class='item ui-widget-content selected resizable' style='top: " + template.y + "px; left: " + template.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + template.resource_absoluteurl + "' width='" + template.width + "px' height='" + template.height + "px'  /><span class='img-info'></span></div>";
                $('#' + area.id).append(div);
                template.addObject($(this), 'img');
                template.imageMode();
                template.addImageEvent();
                template.hidePlaceholder($('#' + area.id));
            } else if (result.typeCode == "css") {
                $("#TemplateModel_Template_Attached_File1_URL").val(result.resource_url);
                $("#TemplateModel_Attached_File1_absoluteUri").val(result.resource_absoluteurl);

                fn_repalce_btncssupload();
                template_areas = [];
                tb_area.forEach(function (elem) {
                    var sort = 1;
                    $("#wrap").find(">div").each(function () {
                        if ($(this).attr('idx') == elem.Area_ID) {
                            $("#wrap>div[idx='" + elem.Area_ID + "']").css("display", "block");
                            if ($(this).hasClass('templatearea')) {
                                var obj = new Object();
                                obj.Area_ID = elem.Area_ID;
                                obj.Size_Height = $(this).height();
                                obj.Size_Width = $(this).width();
                                obj.Color = $(this).attr("color_val");
                                obj.Sort = sort;
                                template_areas.push(obj);
                            }
                        }
                        sort++;
                    });
                });
            } else if (result.typeCode == "js") {
                $("#TemplateModel_Template_Attached_File2_URL").val(result.resource_url);
                $("#TemplateModel_Attached_File2_absoluteUri").val(result.resource_absoluteurl);
                fn_repalce_btnjsupload();
                template_areas = [];
                tb_area.forEach(function (elem) {
                    var sort = 1;
                    $("#wrap").find(">div").each(function () {
                        if ($(this).attr('idx') == elem.Area_ID) {
                            $("#wrap>div[idx='" + elem.Area_ID + "']").css("display", "block");
                            if ($(this).hasClass('templatearea')) {
                                var obj = new Object();
                                obj.Area_ID = elem.Area_ID;
                                obj.Size_Height = $(this).height();
                                obj.Size_Width = $(this).width();
                                obj.Color = $(this).attr("color_val");
                                obj.Sort = sort;
                                template_areas.push(obj);
                            }
                        }
                        sort++;
                    });
                });
            } else if (result.typeCode == "photo") {
                template.org_width = result.width;
                template.org_height = result.height

                if (result.width >= area.width) {
                    template.x = 0;
                    template.y = 0;

                    result.height = parseFloat(result.height * area.width / result.width)

                    result.width = parseFloat(area.width)

                    if (area.height < result.height) {
                        $('#area1').height(result.height);
                        area.height = result.height;
                    }
                }

                template.resource_url = result.resource_url;
                template.resource_absoluteurl = result.resource_absoluteurl;
                template.width = result.width;
                template.height = result.height

                template.setTemplateIdx();

                template.id = 'item_' + template.idx;
                var div = "<div id='" + template.id + "' idx='" + template.idx + "' class='item ui-widget-content selected resizable photo-image' style='top: " + template.y + "px; left: " + template.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + template.resource_absoluteurl + "' width='" + template.width + "px' height='" + template.height + "px'  /><span class='img-info'></span></div>";
                $('#area1').append(div);
                $('#area1').removeClass('selected');


                $('#btnphotoupload').removeClass('btn-primary');
                $('#btnphotoupload').addClass('btn-secondary');
                $('#btnphotoupload').removeClass('empty');
                $('#btnphotoupload').text('삭제');

                template.addObject($(this), 'photo');
                template.imageMode();
                template.addImageEvent();

                $("#call-cropImg").modal("hide");
            } else if (result.typeCode == "profile") {
                template.org_width = result.width;
                template.org_height = result.height

                if (result.width >= area.width) {
                    template.x = 0;
                    template.y = 0;

                    result.height = parseFloat(result.height * area.width / result.width)

                    result.width = parseFloat(area.width)

                    if (area.height < result.height) {
                        $('#area1').height(result.height);
                        area.height = result.height;
                    }
                }

                template.resource_url = result.resource_url;
                template.resource_absoluteurl = result.resource_absoluteurl;
                template.width = result.width;
                template.height = result.height

                template.setTemplateIdx();

                template.id = 'item_' + template.idx;
                var div = "<div id='" + template.id + "' idx='" + template.idx + "' class='item ui-widget-content selected resizable profile-image' style='top: " + template.y + "px; left: " + template.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + template.resource_absoluteurl + "' width='" + template.width + "px' height='" + template.height + "px'  /><span class='img-info'></span></div>";
                $('#area18').append(div);
                $('#area18').removeClass('selected');


                $('#btnprofileupload').removeClass('btn-primary');
                $('#btnprofileupload').addClass('btn-secondary');
                $('#btnprofileupload').removeClass('empty');
                $('#btnprofileupload').text('삭제');

                template.addObject($(this), 'profile');
                template.imageMode();
                template.addImageEvent();

                $("#call-cropImg").modal("hide");
            } else if (result.typeCode == "mainimage") {
                $("#imgMainImageUri").attr("src", result.resource_absoluteurl);
                $("input[type=hidden][name=Main_ImageAbsoluteUri]").val(result.resource_absoluteurl);
                $("input[type=hidden][name=Main_Image_URL]").val(result.resource_url);
            }
        } else {
            alert(result.message);
        }
        $(".loader").hide();
    });
});

function ProdCateRenderSelected() {

    $("#sc li").remove();
    $("select[name=SelectedProductCategories] :selected").map(function (i, el) {
        $("#sc").append("<li id='" + $(el).val() + "'>" + $(el).text() + "<span class='ml-2'><i class='fas fa-times table-primary-red'></i></span></li>");
    });

    $('#sc li .ml-2').each(function (index, item) {
        $(item).click(function (e) {
            var code = $(this).parent().attr('id');
            $("select[name=SelectedProductCategories] option").map(function (i, el) {
                if ($(el).val() == code) {
                    $(el).prop("selected", false);
                }
            })
            ProdCateRenderSelected();
        });
    });
}

var template_areas = [];
var area = {};
var objects = [];

var tb_area = [];
var item = function () {
    return {
        item_id: null,
        resource_id: null,
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
$(document).on('click', '.w25 li p', function () {
    $(".w25 li img").removeClass("selected");
    $(this).addClass("selected");
    template.initItem();
    template.copyTitle();
});
$(document).on('click', '.w25 li img', function () {
    $(".w25 li p").removeClass("selected");
    $(".w25 li img").removeClass("selected");
    $(this).addClass("selected");

    var mydata = {
        "Template_ID": $(this).attr('id'),
        "Current_Product_Code": $("#Original_Product_Code").val(),
        "Product_Code": $(this).attr('code'),
        "Product_Category_Code": $(this).attr('category')
    };
    $.ajax({
        type: "POST",
        url: $("#GetTemplateList").attr("href"),
        data: mydata,
        dataType: "json",
        success: function (result) {

            template.initItem();
            template.setArea(result.area);
            objects = result.template;
            template.setItem();
            template.copyTitle();
        }
    });
});

var template = {
    idx: 0,
    id: null,
    width: 0,
    height: 0,
    x: 0,
    y: 0,

    addImage: function (e) {
        tempFileUpload('templateImage');
    },
    addImageEvent: function (e) {

        $('#' + template.id).resizable({
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

                template.pid = $("#" + template.id).parent('div').attr('id');

                template.showInfo($(this), 'img');
            },
            stop: function (event, ui) {
                $(this).children(".ui-resizable-handle").addClass('resizabled')
                $(this).find($('.img-info')).css('display', 'none');
                template.changeObject($(this), 'img');
            }
        });

        $('#' + template.id).draggable({
            containment: "#" + area.id,
            cursor: "move",
            snap: ".templatearea",
            snapTolerance: 1,
            drag: function (event, ui) {
                $(this).children(".ui-resizable-handle").removeClass('resizabled');

                template.showInfo($(this), 'img');
                $(".guideline").show();
            },
            stop: function (event, ui) {
                $(this).children(".ui-resizable-handle").addClass('resizabled');
                $(this).find($('.img-info')).css('display', 'none');
                template.changeObject($(this), 'img');
                $(".guideline").hide();
            }
        });

        $('#' + template.id).mousedown(function (e) {

            $(".ui-resizable-handle").removeClass('resizabled');
            $(".resizable").removeClass("selected");
            $(this).addClass("selected");
            $(this).children(".ui-resizable-handle").addClass('resizabled');
            $(".ui-resizable-handle").hide();
            $(this).children(".ui-resizable-handle").show();

            area.id = $(this).parent('div').attr('id')
            template.id = $(this).attr('id');
            template.x = e.pageX - $(this).parent('div').offset().left;
            template.y = e.pageY - $(this).parent('div').offset().top;
            $(".matchinfo").removeClass("selected");
            template.imageMode();
            template.showInfo($(this), 'img');
            e.stopPropagation();

        });

        $('#' + template.id).mouseup(function (e) {
            $(this).find($('.img-info')).css('display', 'none');
        });

        $(".ui-resizable-handle").removeClass('ui-icon ui-icon-gripsmall-diagonal-se');

        $('#' + template.id).children(".ui-resizable-handle").addClass('resizabled');

    },
    setTemplateIdx: function () {
        if ($('.item').length > 0) {
            var tmp_idx = 1;
            $('.item').each(function (index, obj) {
                tmp_idx = parseInt($("#" + obj.id).attr('idx')) + 1;
                if (template.idx < tmp_idx) {
                    template.idx = tmp_idx;
                }
            });
        } else {
            template.idx = 1;
        }
    },
    changeText: function (target) {
        objects.forEach(function (elem) {
            if (elem.id == $(".item.selected").attr('id') && $(".matchinfo.selected").attr('idx') == $(target).attr('idx')) {
                elem.chracterset = $(target).val();
            }
        });
        var n = $(target).attr('idx');
        var text = template.matchText($(target).val());
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

                template.pid = $(".item.selected").parent('div').attr('id');
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

        template.setTemplateIdx();

        var text = template.matchText(key);

        template.id = 'item_' + template.idx;
        template.txtid = 'addtext_' + template.idx;
        var div = "<div id='" + template.id + "' idx='" + template.idx + "' class='item ui-widget-content selected resizable' style='top: " + template.y + "px; left: " + template.x + "px; position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><div class='text'  >" + text + "</div><span class='txt-info'></span></div>";
        var matchinfo = "<input type='text' id='" + template.txtid + "' idx='" + template.idx + "' class='form-control form-control-sm matchinfo selected' value='" + key + "'>"


        $('#' + area.id).append(div);
        $('#divMatch').append(matchinfo);

        $("#" + template.txtid).on("focus", function (e) {
            $(document).unbind('keydown');
        });
        $("#" + template.txtid).on("blur", function (e) {
            $(document).on("keydown", function (event) {
                if (event.keyCode == 46) {

                    setActivePhotoButton();
                    
                    template.pid = $(".item.selected").parent('div').attr('id');
                    $(".item.selected").remove();
                    $(".matchinfo.selected").remove();
                    template.delObject();
                    template.initMode();
                    if ($("#" + template.pid + " .item").length < 1) {
                        template.resizeArea('delete', template.pid);
                    }
                    template.showPlaceholder($('#' + template.pid));
                }
            });
        });

        $('.item.selected>.text').css('font-family', "'Noto Sans KR', sans-serif");

        template.org_height = parseFloat($('#' + template.id).outerHeight());
        template.org_width = parseFloat($('#' + template.id).outerWidth());
        template.height = parseFloat($('#' + template.id).outerHeight());
        template.width = parseFloat($('#' + template.id).outerWidth());
        template.characterset = key;

        template.addObject($(this), 'txt');

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

        template.textMode();
        template.addTextEvent();

        template.hidePlaceholder($('#' + area.id));
    },
    addTextEvent: function (e) {
        $("#" + template.id).resizable({
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

                template.pid = $("#" + template.id).parent('div').attr('id');
                $("#FontSize").val(parseInt($(this).children('.text').css("font-size")));
                template.showInfo($(this), 'txt');
            },
            stop: function (event, ui) {
                $(this).find($('.txt-info')).css('display', 'none');
                template.changeObject($(this), 'txt');
            }

        });

        $('#' + template.id).draggable({
            containment: "#" + area.id,
            cursor: "move",
            snap: ".templatearea",
            snapTolerance: 1,
            start: function (event, ui) {

                $(".ui-resizable-handle").removeClass('resizabled');
            },
            drag: function (event, ui) {
                template.showInfo($(this), 'txt');
                $(".guideline").show();
            },
            stop: function (event, ui) {
                $(this).children(".ui-resizable-handle").addClass('resizabled');
                $(this).find($('.txt-info')).css('display', 'none');
                template.changeObject($(this), 'txt');
                $(".guideline").hide();
            }
        });

        $('#' + template.id).mousedown(function (e) {
            $(".ui-resizable-handle").removeClass('resizabled');
            $(".resizable").removeClass("selected");
            $(this).addClass("selected");
            $(this).children(".ui-resizable-handle").addClass('resizabled');
            $(".ui-resizable-handle").hide();
            $(this).children(".ui-resizable-handle").show();

            area.id = $(this).parent('div').attr('id')
            template.id = $(this).attr('id');
            template.x = e.pageX - $(this).parent('div').offset().left;
            template.y = e.pageY - $(this).parent('div').offset().top;

            $(".matchinfo").removeClass("selected");
            $(".matchinfo[idx=" + $(this).attr('idx') + "]").addClass("selected");

            template.textMode();

            template.showInfo($(this), 'txt')

            e.stopPropagation();
        });

        $('#' + template.id).mouseup(function (e) {
            $(this).find($('.txt-info')).css('display', 'none');
        });

        $(".ui-resizable-handle").removeClass('ui-icon ui-icon-gripsmall-diagonal-se');

        $('#' + template.id).children(".ui-resizable-handle").addClass('resizabled');
    },
    bring_front: function (e) {
        var pid = $(".item.selected").parent('div').attr('id');
        $("#" + pid + " > .item").last().after($(".item.selected"));
        template.initObject();
    },
    bring_forward: function (e) {
        var pid = $(".item.selected").parent('div').attr('id');
        $("#" + pid + " > .item.selected").next().after($(".item.selected"));
        template.initObject();
    },
    send_back: function (e) {
        var pid = $(".item.selected").parent('div').attr('id');
        $("#" + pid + " > .item").first().before($(".item.selected"));
        template.initObject();
    },
    send_backward: function (e) {
        var pid = $(".item.selected").parent('div').attr('id');

        $("#" + pid + " > .item.selected").prev().before($(".item.selected"));
        template.initObject();
    },
    changeObject: function (ui, type) {
        template.x = parseFloat(ui.position().left);
        template.y = parseFloat(ui.position().top);
        template.width = parseFloat(ui.outerWidth());
        template.height = parseFloat(ui.outerHeight());

        if (type == 'txt') {
            template.fontsize = parseInt(ui.children('.text').css("font-size"));
        } else {
            template.fontsize = 0;
        }

        objects.forEach(function (elem) {
            if (elem.id == template.id) {
                elem.left = template.x
                elem.top = template.y
                elem.width = template.width
                elem.height = template.height
                elem.fontsize = template.fontsize
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
        obj.resource_url = null;
        obj.pid = $("#" + obj.id).parent('div').attr('id');
        obj.type = type;
        obj.zindex = idx + 1;
        obj.top = template.y;
        obj.left = template.x;
        obj.org_width = template.org_width;
        obj.org_height = template.org_height;
        obj.width = template.width;
        obj.height = template.height;

        if (type == 'img' || type == 'photo' || type == 'profile') {

            obj.resource_url = template.resource_url;
            obj.resource_absoluteurl = template.resource_absoluteurl;
            obj.fontsize = 0;
        }

        if (type == 'txt') {
            obj.fontsize = parseInt($('html').css("font-size"));
            obj.chracterset = template.characterset;
        }

        objects.push(obj);
        template.initObject();
    },
    initObject: function () {

        var idx = 1;
        $('.item').each(function () {
            var obj = new item();
            obj.id = $(this).attr('id');

            objects.forEach(function (elem) {
                if (elem.pid == $("#" + obj.id).parent('div').attr('id') && elem.id == obj.id) {
                    elem.zindex = idx;
                    template.resetLayerButton(elem);
                }
            });
            template.setLayerStatus();
            idx++;
        });
    },
    delObject: function () {

        var tmp_objects = [];

        var idx = 1;

        $('.item').each(function () {
            var obj = new item();
            obj.id = $(this).attr('id');
            objects.forEach(function (elem) {
                if (elem.pid == $("#" + obj.id).parent('div').attr('id') && elem.id == obj.id) {
                    elem.zindex = idx;
                    template.resetLayerButton(elem);
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
        $(".templatearea").removeClass("selected");
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
        template.setTextStatus();
    },
    imageMode: function () {
        template.initMode();

        $("#divDisabled4").css("display", "none");

        template.setImageStatus();
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

                template.resetLayerButton(elem);

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
            var prodCateCode = $("input:radio[id=ProductCategoryCode]:checked").val();
            if (prodCateCode == "PCC03") {
                remittitleImage = "/img/title/tit_remittance_PCC03.png";
            }

            area.id = "area12";
            template.resource_url = remittitleImage;
            template.width = 800;
            template.height = 223;
            template.y = 0;
            template.x = 0;
            template.idx = 1;

            $('#' + area.id).css('height', template.height + "px");

            template.id = 'item_' + template.idx;
            var div = "<div id='" + template.id + "' idx='" + template.idx + "' class='item ui-widget-content selected resizable' style='top: " + template.y + "px; left: " + template.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + template.resource_url + "' width='" + template.width + "px' height='" + template.height + "px'  /><span class='img-info'></span></div>";

            $('#' + area.id).append(div);
            template.addObject($(this), 'img');
            //template.imageMode();
            template.addImageEvent();
            template.hidePlaceholder($('#' + area.id));

            $(".ui-resizable-handle").removeClass('resizabled');
            $(".resizable").removeClass("selected");
            $(".ui-resizable-handle").hide();
        }
    },
    setImageStatus: function () {
        var id = $('.item.selected').attr('id');

        objects.forEach(function (elem) {
            if (elem.id == id) {

                template.resetLayerButton(elem);

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
            template.idx++;
            template.id = elem.id;
            template.pid = elem.pid;
            template.x = elem.left;
            template.y = elem.top;
            template.resource_url = elem.resource_url;
            template.resource_absoluteurl = elem.resource_absoluteurl;
            template.width = elem.width;
            template.height = elem.height;
            if (elem.type == 'img') {
                var div = "<div id='" + template.id + "' idx='" + template.idx + "' class='item ui-widget-content resizable' style='top: " + template.y + "px; left: " + template.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + template.resource_absoluteurl + "' width='" + template.width + "px' height='" + template.height + "px'  /><span class='img-info'></span></div>";
                $('#' + template.pid).append(div);
                area.id = template.pid;
                template.addImageEvent();
                $('#' + template.id).children(".ui-resizable-handle").removeClass('resizabled');
                $('#' + template.id).children(".ui-resizable-handle").css('display', 'none');
                template.hidePlaceholder($('#' + area.id));
            } else if (elem.type == 'photo') {
                var div = "<div id='" + template.id + "' idx='" + template.idx + "' class='item ui-widget-content resizable photo-image ' style='top: " + template.y + "px; left: " + template.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + template.resource_absoluteurl + "' width='" + template.width + "px' height='" + template.height + "px'  /><span class='img-info'></span></div>";
                $('#' + template.pid).append(div);
                template.addImageEvent();
                $('#' + template.id).children(".ui-resizable-handle").removeClass('resizabled');
                $('#' + template.id).children(".ui-resizable-handle").css('display', 'none');

                $('#btnphotoupload').removeClass('btn-primary');
                $('#btnphotoupload').addClass('btn-secondary');
                $('#btnphotoupload').removeClass('empty');
                $('#btnphotoupload').text('삭제');
                template.hidePlaceholder($('#' + area.id));
            } else if (elem.type == 'profile') {
                var div = "<div id='" + template.id + "' idx='" + template.idx + "' class='item ui-widget-content resizable profile-image ' style='top: " + template.y + "px; left: " + template.x + "px;  position:absolute;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><img class='img' src='" + template.resource_absoluteurl + "' width='" + template.width + "px' height='" + template.height + "px'  /><span class='img-info'></span></div>";
                $('#' + template.pid).append(div);
                template.addImageEvent();
                $('#' + template.id).children(".ui-resizable-handle").removeClass('resizabled');
                $('#' + template.id).children(".ui-resizable-handle").css('display', 'none');

                $('#btnprofileupload').removeClass('btn-primary');
                $('#btnprofileupload').addClass('btn-secondary');
                $('#btnprofileupload').removeClass('empty');
                $('#btnprofileupload').text('삭제');
                template.hidePlaceholder($('#' + area.id));
            } else {
                var text = template.matchText(elem.chracterset);

                template.id = 'item_' + template.idx;
                template.txtid = 'addtext_' + template.idx;
                var div = "<div id='" + template.id + "' idx='" + template.idx + "' class='item ui-widget-content resizable' style='top: " + template.y + "px; left: " + template.x + "px;    position:absolute; width:" + template.width + "px;  height:" + template.height + "px;'><span class='topline'></span><span class='rightline'></span><span class='botline'></span><span class='leftline'></span><div class='text'>" + text + "</div><span class='txt-info'></span></div>";
                var matchinfo = "<input type='text' id='" + template.txtid + "' idx='" + template.idx + "' class='form-control form-control-sm matchinfo ' value='" + elem.chracterset + "'>"
                $('#' + template.pid).append(div);
                $('#divMatch').append(matchinfo);
                area.id = template.pid;
                template.addTextEvent();
                $('#' + template.id).css('background-color', elem.bgcolor);
                $('#' + template.id).children(".ui-resizable-handle").removeClass('resizabled');
                $('#' + template.id).children(".ui-resizable-handle").css('display', 'none');
                $('#' + template.id + ">.text").css('font-family', elem.font);
                $('#' + template.id + ">.text").css('font-size', elem.fontsize);
                $('#' + template.id + ">.text").css('color', elem.fontcolor);
                $('#' + template.id + ">.text").css('font-weight', elem.bold_yn ? "bold" : "");
                $('#' + template.id + ">.text").css('font-style', elem.italic_yn ? "italic" : "");
                $('#' + template.id + ">.text").css('text-decoration', elem.underline_yn ? "underline" : "");
                if (elem.horizontal_align == "C") {
                    $('#' + template.id + ">.text").css('text-align', "center")
                } else if (elem.horizontal_align == "R") {
                    $('#' + template.id + ">.text").css('text-align', "right")
                } else if (elem.horizontal_align == "L") {
                    $('#' + template.id + ">.text").css('text-align', "left");
                } else {
                    $('#' + template.id + ">.text").css('text-align', "");
                }
                if (elem.vertical_align == "T") {
                    $('#' + template.id).css('align-items', "flex-start")
                } else if (elem.vertical_align == "M") {
                    $('#' + template.id).css('align-items', "center")
                } else if (elem.vertical_align == "B") {
                    $('#' + template.id).css('align-items', "flex-end");
                } else {
                    $('#' + template.id).css('align-items', "");
                }
                $('#' + template.id + ">.text").css('letter-spacing', elem.between_text / 100 + "em");
                $('#' + template.id + ">.text").css('line-height', elem.between_line + "em");
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
        $(".templatearea").css('height', '100px');
        $(".templatearea").css('background-color', '');
        $(".templatearea").removeAttr("color_val");
        $(".templatearea").removeClass("selected");
        $(".templatearea div").remove();
        $(".templatearea").find('.placeholder').show();

        $("#Preview_URL").val("");
        $("#template_bgcolor").val("");

        $("#divMatch input").remove();
        $(".w25 li p").removeClass("selected");
        $(".w25 li img").removeClass("selected");
        objects = [];

        $('.templatearea').resizable({
            handles: 's'
        });

        $('.ui-resizable-handle.ui-resizable-s').css("display", "none");

        fn_display_EffectCheckbox();
    },

    setArea: function (obj) {

        template.initItem();
        $("#wrap").find(">div").each(function () {
            $(this).css("display", "none");
        });

        var prodCateCode = $("input:radio[id=ProductCategoryCode]:checked").val();
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

                    if ($(this).hasClass('templatearea')) {
                        $('#area' + elem.Area_ID).css('height', elem.Size_Height + "px");
                        $('#area' + elem.Area_ID).css('background-color', elem.Color);
                        $('#area' + elem.Area_ID).attr('color_val', elem.Color);

                        template.hidePlaceholder($('#area' + elem.Area_ID));
                    }
                }
            });
        });
    },
    getTemplateList: function () {
        $("#preTemplateList").css("display", "block");
        $(".w25").empty();
        $(".w25").append("<li><p>새로 만들기</p></li>");
        $('.w25 li p').trigger('click');

        var prodCateCode = $("input:radio[id=ProductCategoryCode]:checked").val();
        var tempArr = JSON.parse($("#TB_Template").text());
        var tb_template = $.grep(tempArr, function (item, index) { return (item.Product_Category_Code == prodCateCode) });

        if (tb_template.length > 0) {
            tb_template.forEach(function (elem) {
                if ($("input:checkbox[name=Photo_YN]").is(":checked") == elem.Photo_YN) {
                    $(".w25").append("<li><img src='" + elem.Main_Image_URL + "' style='width:100%;height:96px;' id='" + elem.Template_ID + "' code='" + elem.Product_Code + "' category='" + elem.Product_Category_Code + "'></li>");
                }
            });
        }
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

function fn_cropImageUpload(croptype, img_data) {
    tempFileUpload(croptype, img_data);
}

//파일 업로드 팝업
function tempFileUpload(typeCode, img_data) {
    $("#uploadFrm")[0].reset();
    $("#uploadFrm input[name=TypeCode]").val(typeCode);
    if (typeCode == 'photo' || typeCode == 'mainimage' || typeCode == 'profile' ) {
        $("#uploadFrm input[name=imageData]").val(img_data);
        $("#uploadFrm").submit();
    } else {
        $("#uploadFrm #file").click();
    }
}

function fn_addCSS() {
    tempFileUpload('css');
}
function fn_delCSS() {
    template_areas = [];
    tb_area.forEach(function (elem) {
        var sort = 1;
        $("#wrap").find(">div").each(function () {
            if ($(this).attr('idx') == elem.Area_ID) {
                $("#wrap>div[idx='" + elem.Area_ID + "']").css("display", "block");
                if ($(this).hasClass('templatearea')) {
                    var obj = new Object();
                    obj.Area_ID = elem.Area_ID;
                    obj.Size_Height = $(this).height();
                    obj.Size_Width = $(this).width();
                    obj.Color = $(this).attr("color_val");
                    obj.Sort = sort;
                    template_areas.push(obj);
                }
            }
            sort++;
        });
    });
    $("#TemplateModel_Template_Attached_File1_URL").val("");
    fn_repalce_btncssupload();
}

function fn_addJS() {
    tempFileUpload('js');
}
function fn_delJS() {
    template_areas = [];
    tb_area.forEach(function (elem) {
        var sort = 1;
        $("#wrap").find(">div").each(function () {
            if ($(this).attr('idx') == elem.Area_ID) {
                $("#wrap>div[idx='" + elem.Area_ID + "']").css("display", "block");
                if ($(this).hasClass('templatearea')) {
                    var obj = new Object();
                    obj.Area_ID = elem.Area_ID;
                    obj.Size_Height = $(this).height();
                    obj.Size_Width = $(this).width();
                    obj.Color = $(this).attr("color_val");
                    obj.Sort = sort;
                    template_areas.push(obj);
                }
            }
            sort++;
        });
    });
    $("#TemplateModel_Template_Attached_File2_URL").val("");
    fn_repalce_btnjsupload();
}

function fn_repalce_btncssupload() {
    if ($("#TemplateModel_Template_Attached_File1_URL").val() != "") {
        $('#btncssupload').removeClass('btn-primary');
        $('#btncssupload').addClass('btn-secondary');
        $('#btncssupload').removeClass('empty');
        $('#btncssupload').text('삭제');
    } else {
        $('#btncssupload').removeClass('btn-secondary');
        $('#btncssupload').addClass('btn-primary');
        $('#btncssupload').addClass('empty');
        $('#btncssupload').text('추가');
    }
    if ($("#divtemplate").hasClass('show')) {
        loadcustomjscssfile('css');
    } else {
        removecustomjscssfile('css');
    }
}

function fn_repalce_btnjsupload() {

    if ($("#TemplateModel_Template_Attached_File2_URL").val() != "") {
        $('#btnjsupload').removeClass('btn-primary');
        $('#btnjsupload').addClass('btn-secondary');
        $('#btnjsupload').removeClass('empty');
        $('#btnjsupload').text('삭제');
    } else {
        $('#btnjsupload').removeClass('btn-secondary');
        $('#btnjsupload').addClass('btn-primary');
        $('#btnjsupload').addClass('empty');
        $('#btnjsupload').text('추가');
        
    }
    fn_display_EffectCheckbox();
    if ($("#divtemplate").hasClass('show')) {
        loadcustomjscssfile('js');
    } else {
        removecustomjscssfile('js');
    }
};
function fn_display_EffectCheckbox() {
    if ($("#TemplateModel_Template_Attached_File2_URL").val() != "") {
        $('#design_header span').show();
    } else {
        $('#design_header span').hide();
    }
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

function loadcustomjscssfile(filetype) {
    if (filetype == "js") {
        var filename = $("#TemplateModel_Attached_File2_absoluteUri").val();
        if (filename != "") {
            var fileref = document.createElement('script')
            fileref.setAttribute("type", "text/javascript")
            fileref.setAttribute("src", filename)
            fileref.setAttribute("id", "customjs")
            fileref.onload = function () {
                startEffect();
            }
        }
    }
    else if (filetype == "css") {
        var filename = $("#TemplateModel_Attached_File1_absoluteUri").val();
        if (filename != "") {
            var fileref = document.createElement("link")
            fileref.setAttribute("rel", "stylesheet")
            fileref.setAttribute("type", "text/css")
            fileref.setAttribute("href", filename)
            fileref.setAttribute("id", "customcss")
        }
    }
    if (typeof fileref != "undefined") {
        document.getElementsByTagName("head")[0].appendChild(fileref);
    }
}

function removecustomjscssfile(filetype) {
    if (filetype == "js") {
        var fileref = document.getElementById('customjs');
    } else if (filetype == "css") {
        var fileref = document.getElementById('customcss');
    }
    if (fileref != null)
        fileref.parentNode.removeChild(fileref);

}

function initTemplateLayer() {
    var mapfields = JSON.parse($("#MappingFields").text());
    var prodCateCode = $("input:radio[id=ProductCategoryCode]:checked").val();
    $("#selMatchInfo")
        .empty()
        .append("<option value=''>매칭정보추가</option>");
    var mappingfield = {};

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
        //인사말 기본 템플릿 변경
        if (item.MappingField == "Greetings" && item.Product_Category_Codes.includes(prodCateCode)) {
            $("#Greetings").val(item.DefaultValue);
        }

    }
    

    /* Context Men - Start */
    $.contextMenu('destroy');

    $.contextMenu({
        selector: '.templatearea',
        trigger: 'right',
        callback: function (key, options, e) {
            if (e.which == 1) {
                template.initClass();

                if (key == "add_image") {
                    template.addImage();
                } else {
                    template.addText(key);
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
                        template.initClass();
                        template.addImage();
                        break;
                    case "trash":
                        template.pid = $(".item.selected").parent('div').attr('id');
                        $(".item.selected").remove();
                        $(".matchinfo.selected").remove();
                        template.delObject();
                        template.initMode();
                        if ($("#" + template.pid + " .item").length < 1) {
                            template.resizeArea('delete', template.pid);
                        }
                        template.showPlaceholder($('#' + template.pid));
                        break;
                    case "bring_front":
                        template.bring_front();

                        break;
                    case "bring_forward":
                        template.bring_forward();
                        break;
                    case "send_back":
                        template.send_back();
                        break;
                    case "send_backward":
                        template.send_backward();
                        break;
                    default:
                        template.initClass();
                        template.addText(key);
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
    /* Context Men - End */

    /* 정보 입력영역 */
    $("div.dvInfo div.card.mb-2").each(function (index, item) {
        if ($(item).hasClass(prodCateCode)) {
            $(item).show();
        } else {
            $(item).hide();
        }
    });

    //갤러리 영역
    $("#area6 div.gallery_type01").hide();
    $("#area6 div.gallery_type01").each(function (index, item) {
        if ($(item).hasClass(prodCateCode)) {
            $(item).show();
        } else {
            $(item).hide();
        }
    });
    //방명록 영역
    $("#area15 div.message_wrap").hide();
    $("#area15 div.message_wrap").each(function (index, item) {
        if ($(item).hasClass(prodCateCode)) {
            $(item).show();
        } else {
            $(item).hide();
        }
    });
}

function setActivePhotoButton() {
    if ($(".item.selected").hasClass('photo-image')) {
        $('#btnphotoupload').addClass('btn-primary');
        $('#btnphotoupload').removeClass('btn-secondary');
        $('#btnphotoupload').addClass('empty');
        $('#btnphotoupload').text('추가');
    }
    if ($(".item.selected").hasClass('profile-image')) {
        $('#btnprofileupload').addClass('btn-primary');
        $('#btnprofileupload').removeClass('btn-secondary');
        $('#btnprofileupload').addClass('empty');
        $('#btnprofileupload').text('추가');
    }

}