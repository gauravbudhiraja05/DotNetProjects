$(document).ready(function () {
    //var deptId = getParameterByName("DepartmentId");
    var url = window.location.href;
    var deptId = url.split('/')[5];
    if (deptId != null && deptId != "") {
        //$('.selectpicker option[id=' + deptId +']').attr('selected', 'selected');
        //var contactNumber = $("#ddlSelectDepartment option:selected").attr('telephoneatt');
        //$('#div_header').html($('.selectpicker').val());
        //$('.dep_number').html("Tel: " + contactNumber);
        //$('.dep_number').attr("href", "tel:" + contactNumber);

        //var imagename = $('.selectpicker option[id=' + deptId + ']').attr('imageNameatt');
        //$(".deptBanner").css("background-image", "url(/uploads/Admin/Department/" + imagename +")"); 
        GetIntranetDepartmentDetailsByDepartmentWise(deptId);
        GetIntranetNewsDetailByDepartmentWise(deptId);
    }

    //ddlSelectDepartment
    $('#ddlSelectDepartment').on('change', function () {
        var deptId = parseInt(($("#ddlSelectDepartment option:selected").attr('id')));
        var fileName = $("#ddlSelectDepartment option:selected").attr('fileNameattr');
        var type = $("#ddlSelectDepartment option:selected").attr('docTypeAttr');
        var linkDestination = $("#ddlSelectDepartment option:selected").attr('linkDestinationAttr');

        if (type == "Document") {
            var extension = fileName.substr((fileName.lastIndexOf('.') + 1));
            if (extension == "pdf") {
                window.open('/fileserver/Uploads/Documents/' + fileName + '', '_blank');
            }

            else {
                window.open('/FrontEndHome/GetDocumentToDownload?documentName=' + fileName + '', '_blank');
            }
        }

        else {
            window.open(linkDestination, '_blank');
        }
        
    });

    $(".make_dep_btn").on('click', function () {
        var deptId = $('#ddlSelectDepartment').find('option:selected').attr('id')
        //var deptId = getParameterByName("DepartmentId");
        var userId = $('#FrontEndUserDetails_Id').val();

        $.ajax({
            type: "Post",
            url: "/FrontEndHome/SaveMydepartment",
            data: { deptId: deptId, userId: userId },
            success: function (Data) {
                debugger;
                $("#myDepartmentlnk").attr('onClick', "window.location.href='/FrontEndHome/IntranetDepartment?DepartmentId=" + deptId + "'");
                $("#myDepartmentlnk").removeClass("isDisabled");
                onSuccess(Data);
            },
            error: function (response) {

            }
        });
    });





});

function onSuccess(data) {
    var d = data;
    if (d.result.isSuccess) {
        swal("Department is Updated Successfully");
    }

}

function GetIntranetDepartmentDetailsByDepartmentWise(departmentId) {

    $.get('/FrontEndHome/GetIntranetDepartmentDetailsByDepartmentWise/' + departmentId, function (data) {

        $('#partialDepartment').html(data);
        $('#partialDepartment').show();

    });
}

function GetIntranetNewsDetailByDepartmentWise(departmentId) {

    $.get('/FrontEndHome/GetIntranetNewsDetailByDepartmentWise/' + departmentId, function (data) {

        $('#partialNewsByDepartment').html(data);
        $('#partialNewsByDepartment').show();
        setTimeout(function () {
            equalheight('.news_slide_item > .inner');
        }, 300);


        var items = $('.news_items_slider .news_slide_item');
        if (items.length > 1) {
            $(".news_items_slider").owlCarousel({
                autoplayTimeout: 4500,
                autoplayHoverPause: true,
                autoplay: false,
                items: 4,
                margin: 20,
                dots: false,
                nav: true,
                loop: false,
                responsive: {
                    0: {
                        items: 2
                    },
                    768: {
                        items: 3
                    },
                    979: {
                        items: 3
                    },
                    1199: {
                        items: 4
                    }
                }

            });
        } else {
        }


    });
}


function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
