$(function () {
    $.getJSON('/api/country', function (result) {
        var countries = result;

        //Country
        var countryInput = $(document).find('select.countrypicker');
        var countryList = "";

        //Phone
        var phoneInpute = $(document).find('select.phonepicker');
        var phoneList = "";


        //All country pickers
        for (i = 0; i < countryInput.length; i++) {

            //check if flag
            flag = countryInput.eq(i).data('flag');

            if (flag) {
                countryList = "";

                //for each build list with flag
                $.each(countries, function (index, country) {
                    var flagIcon = "/images/flags/" + country.alpha2Code + ".png";
                    countryList += "<option value=\"" + country.alpha2Code + "\" data-content=\"<div class=\'div_selectcountry_dilaco\' style='background-image:url(" + flagIcon + ");'></div><div>" + country.shortName + "</div>\"></option>";
                });
            } else {
                countryList = "";

                //for each build list without flag
                $.each(countries, function (index, country) {
                    countryList += "<option data-country-code='" + country.alpha2Code + "' data-tokens='" + country.alpha2Code + " " + country.shortName + "' value='" + country.shortName + "'>" + country.shortName + "</option>";
                });


            }

            //append country list
            countryInput.eq(i).html(countryList);


            //check if default
            def = countryInput.eq(i).data('default');
            //if there's a default, set it
            if (def) {
                countryInput.eq(i).val(def);
            }

            //Nagyon fontos meghívni a refresht!!
            $('.countrypicker').selectpicker('refresh');
        }

        //All phone pickers
        for (i = 0; i < phoneInpute.length; i++) {

            //check if flag
            flag = phoneInpute.eq(i).data('flag');

            if (flag) {
                phoneList = "";

                //for each build list with flag
                $.each(countries, function (index, country) {
                    var flagIcon = "/images/flags/" + country.alpha2Code + ".png";
                    phoneList += "<option value = \"" + country.callingCode + "\" data-content = \"<div class=\'div_selectcountry_dilaco\' style='background-image:url(" + flagIcon + ");'></div><div class='float-left'>" + country.callingCode + "</div><small class='phone_small_dilaco'> " + country.shortName + "</small>\"</option>";
                });
            } else {
                phoneList = "";

                //for each build list without flag
                $.each(countries, function (index, country) {
                    phoneList += "<option data-country-code='" + country.alpha2Code + "' data-tokens='" + country.alpha2Code + " " + country.shortName + "' value='" + country.shortName + "'>" + country.shortName + "</option>";
                });


            }

            //append country list
            phoneInpute.eq(i).html(phoneList);


            //check if default
            def = phoneInpute.eq(i).data('default');
            //if there's a default, set it
            if (def) {
                phoneInpute.eq(i).val(def);
            }

            //Nagyon fontos meghívni a refresht!!
            $('.phonepicker').selectpicker('refresh');
        }

        //Phone Only numbers show no short_shortname
        phone_small_dilaco_hidden();

        $('.phonepicker').on('hidden.bs.select', function (e) {
            phone_small_dilaco_hidden();
        });

        function phone_small_dilaco_hidden() {
            $('.filter-option-inner .phone_small_dilaco').addClass("invisible");
        }

    });

});