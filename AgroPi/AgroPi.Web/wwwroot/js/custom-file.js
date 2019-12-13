function uploadFiles(inputId) {

    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    formData.append("file", files[0]);


    //startUpdatingProgressIndicator();

    $.ajax(
        {
            url: "/api/File",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                console.log(data);
                //stopUpdatingProgressIndicator();
                if (data === "success") {
                    console.log("Files Uploaded!");
                }
            }
        }
    );
}


function UploadProfilePicture(inputId) {

    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    //id lekérése URL-ből
    var id = getQueryVariable("id");

    formData.append("file", files[0]);

    //Progress bar azonosításáhozhoz saját Guid készítése
    var ProgressGUID = guid();

    startUpdatingProgressIndicator(ProgressGUID);

    //Elöször feltöltjük a képet a szerverre
    $.ajax(
        {
            url: "/api/File?&fileprogressguid=" + ProgressGUID,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (response1) {

                //console.log(response1); DEBUG!!!

                //Progress lekérdezések leállítása
                stopUpdatingProgressIndicator();
                if (response1.satusCodeHTML === 201) {
                    //Sikeres feltöltés után meghívjuk a profil kép frisítése funkciót
                    setProfilePictureAJAX(id, response1);
                }
            }
        }
    );
    function setProfilePictureAJAX(id, response1) {
        $.ajax(
            {
                type: "POST",
                url: "Edit?handler=UpdateProfilePicture&id=" + id,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(response1.fileHeader),
                headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                success: function (response2) {
                    //új kép lekérése
                    $(".card_img_profile").attr("src", "/api/File/" + response1.fileHeader.id);
                    $('.input-group').find(':text').val(null);

                    //console.log(response2); DEBUG!!

                }
            });
    }

}

//Progress bar állása
var intervalId;

function startUpdatingProgressIndicator(ProgressGUID) {
    $("#progress_bar").removeClass("invisible");

    intervalId = setInterval(
        function () {
            // We use the POST requests here to avoid caching problems (we could use the GET requests and disable the cache instead)
            $.post(
                "/api/FileProgress?id=" + ProgressGUID,
                function (progress) {
                    $(".progress-bar").css({ width: progress + "%" });
                    $(".progress-bar").attr("aria-valuenow", progress);
                }
            );
        },
        500 // milliseconds 0,5 sec
    );
}

function stopUpdatingProgressIndicator() {
    clearInterval(intervalId);

    $("#progress_bar").addClass("invisible");
    $(".progress-bar").css({ width: "0%" });
    $(".progress-bar").attr("aria-valuenow", 0);

    $('#exampleModal').modal('hide');

    $("#change_button").css("visibility", "hidden");
}

$(function () {

    // We can attach the `fileselect` event to all file inputs on the page
    $(document).on('change', ':file', function () {

        var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [numFiles, label]);

    });

    // We can watch for our custom `fileselect` event like this
    $(document).ready(function () {
        $(':file').on('fileselect', function (event, numFiles, label) {

            var input = $(this).parents('.input-group').find(':text'),
                log = numFiles > 1 ? numFiles + ' files selected' : label;

            if (input.length) {
                input.val(log);
            } else {
                if (log) alert(log);
            }

        });
    });
    $("#UploadButton").click(function () {
        UploadProfilePicture("file-input");
    });
});


//profilkép változtatás ikon megjelenítése 
$(".card_img_profile").hover(function () {
    $("#change_button").css("visibility", "visible");
}, function () {
    $("#change_button").css("visibility", "hidden");
});
$("#change_button").hover(function () {
    $("#change_button").css("visibility", "visible");
}, function () {});
