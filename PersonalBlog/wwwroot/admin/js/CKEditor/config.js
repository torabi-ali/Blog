$(document).ready(function () {
    evaluate();

    var editor = CKEDITOR.instances.CKEditor;
    if (!editor) {
        CKEDITOR.replace('CKEditor');
    }

    CKEDITOR.editorConfig = function (config) {
        config.contentsLangDirection = 'rtl';
        config.language = 'en';
        config.height = '30em';
    };

    CKEDITOR.instances["CKEditor"].on('blur', function (e) {
        if (e.editor.getData() != $("#div_" + e.editor.name).html()) {
            $("#CKEditor").html(e.editor.getData());
            evaluate();
        }
    });
});

function evaluate() {
    $('.seoValueNotifier').trigger("change");
}

$(document).ready(function () {
    $(".seoValueNotifier, #CKEditor").change(function () {
        var form = $(this).parents();
        var data = form.find(":input").serializeArray();

        $.ajax({
            url: "/Admin/Blog/SEO",
            dataType: "json",
            type: "POST",
            async: true,
            cache: false,
            delay: 250,
            data: data,
            success: function (result) {
                var tempScore = result.totalScore;
                if (tempScore == 0) {
                    tempScore++;
                }
                else if (tempScore == 100) {
                    tempScore--;
                }

                var red = (100 - tempScore) * 255 / 100;
                var green = tempScore * 255 / 100;
                var blue = (Math.abs((red - green) + 1) / 10);
                $('#SeoScore').text(tempScore);
                $('.seo-score').css('background-color', 'rgb(' + Math.round(red) + ',' + Math.round(green) + ',' + Math.round(blue) + ')');

                $('#TitleMessages').empty();
                result.titleMessages.forEach(function (item) {
                    var cssClass = (item.isError == true) ? 'errorMessage' : 'warningMessage';
                    $('#TitleMessages').append('<li class="' + cssClass + '">' + item.message + '</li>')
                });

                $('#KeywordMessages').empty();
                result.keywordMessages.forEach(function (item) {
                    var cssClass = (item.isError == true) ? 'errorMessage' : 'warningMessage';
                    $('#KeywordMessages').append('<li class="' + cssClass + '">' + item.message + '</li>')
                });

                $('#UrlMessages').empty();
                result.urlMessages.forEach(function (item) {
                    var cssClass = (item.isError == true) ? 'errorMessage' : 'warningMessage';
                    $('#UrlMessages').append('<li class="' + cssClass + '">' + item.message + '</li>')
                });

                $('#MetaDescriptionMessages').empty();
                result.metaDescriptionMessages.forEach(function (item) {
                    var cssClass = (item.isError == true) ? 'errorMessage' : 'warningMessage';
                    $('#MetaDescriptionMessages').append('<li class="' + cssClass + '">' + item.message + '</li>')
                });

                $('#TextMessages').empty();
                result.textMessages.forEach(function (item) {
                    var cssClass = (item.isError == true) ? 'errorMessage' : 'warningMessage';
                    $('#TextMessages').append('<li class="' + cssClass + '">' + item.message + '</li>')
                });
            }
        });
    });
});