/*------------*/
/* Tag Picker */
/*------------*/
//Tag Picker engedélyezése csak a szükséges oldalakon
function EnableTagPicker() {

    var allEnabledTags = [];
    var UserTags = [];

    //id lekérése URL-ből
    var id = getQueryVariable("id");

    //Tag Picker inizializálás
    var tagPicker = $('#tag_picker').magicSuggest({
        placeholder: 'Válassz taget',
        noSuggestionText: 'Nincs tag'
    });


    /*Validation Own Style*/
    if ($("#login_form_validation > ul > li[style='display:none']").length === 0) {
        $("#login_form_validation").addClass("alert alert-danger");
    }

    //AJAX-os lekérdezés az összes tag-ről
    $.ajax({
        type: 'GET',
        url: "Edit?handler=AllTags&id=" + id,
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Adatok betöltése egy tömbe
            for (var i = 0; i < data.length; i++) {
                allEnabledTags[allEnabledTags.length] = data[i].name;
            }

            //Tag Picker adatfrissítés
            tagPicker.setData(allEnabledTags);


            //AJAX-os lekérdezés az adott felhasználohoz tartozó tag-ekről
            $.ajax({
                type: 'GET',
                url: "Edit?handler=Tags&id=" + id,
                dataType: "json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        UserTags[UserTags.length] = data[i].name;
                    }
                    tagPicker.setValue(UserTags);
                },
                failure: function (response) {
                    //TUDO!!
                    alert("Failure a tag-ek lekérdezésnél");
                },
                error: function (data) {
                    //TUDO
                    alert("Error a tag-ek lekérdezésénél");
                } //End of AJAX error function  
            });


        },
        failure: function (response) {
            //TUDO!!
            alert("Failure az összes tag lekérdezésénél");
        },
        error: function (data) {
            //TUDO
            alert("Error az összes tag lekérdezésénél");
        } //End of AJAX error function  
    });


    //Events
    $(tagPicker).on('selectionchange', function (e, m) {

        $.ajax({
            type: "POST",
            url: "Edit?handler=Tags&id=" + id,
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: "{Tags: "+JSON.stringify(this.getValue()) +"}",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            success: function (result) {
                console.log('Data received: ');
                console.log(result);
            },
            failure: function (response) {
                //TUDO!!
                alert("Failure a tag-ek feltöltés");
            },
            error: function (data) {
                //TUDO
                alert("Error a tag-ek feltöltése");
            } //End of AJAX error function  
        });
    });
}