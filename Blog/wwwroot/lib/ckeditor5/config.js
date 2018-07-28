$(document).ready(function () {
    ClassicEditor.create(document.querySelector('#CKEditor'))
        .then(editor => {
            console.log(editor);
        })
        .catch(error => {
            console.error(error);
        });
});

$(document).ready(function () {
    CKEDITOR.instances["CKEditor"].on('blur', function (e) {
        if (e.editor.getData() != $("#div_" + e.editor.name).html()) {
            $("#CKEditor").html(e.editor.getData());
            $("#CKEditor").change();
        }
    });

    $(".seoValueNotifier, #CKEditor").change(function () {
        var form = $(this).parents();
        var data = form.find(":input").serializeArray();
        //replace name of array keys for solve greater than index of 0
        for (i = 0; i < data.length; i++) {
            data[i].name = data[i].name.replace(/\d/i, "0")
        }
        $.ajax({
            url: "/Admin/Post/CheckSEO",
            type: "post",
            data: data,
            success: function (result) {
                form.find(".seoResult").html(result);
            }
        });
    });
});    
