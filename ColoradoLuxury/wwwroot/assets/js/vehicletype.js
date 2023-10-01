function ChangePassengerCount(event) {
    console.log($(event).val());

    if ($(event).val() === "") {
        $(".humanSuitcase").find('span:first').text(0);
    }
    else {
        $(".humanSuitcase").find('span:first').text($(event).val());
    }
}

function ChangeSuitcasesCount(event) {
    console.log($(event).val());
    console.log($(".humanSuitcase").find('span')[1]);

    if ($(event).val() === "") {
        $(".humanSuitcase .suitcases-count").text(0);
    }
    else {
        $(".humanSuitcase .suitcases-count").text($(event).val());
    }
}

function ChangeVehicleType(event) {


    let passengersCount = $("#passengersSelect").val();
    let suitcasesCount = $("#suitcases").val();

    AjaxPost("/Home/ChangeVehicleType/", { id: $(event).val(), passengersCount: passengersCount, suitcasesCount: suitcasesCount }, true, true, 'json', 'application/x-www-form-urlencoded; charset=UTF-8', (response) => {
        console.log(response);
        let vehicleType = null;
        $(".allvehicletypes").empty();
        for (var i = 0; i < response.distanceAmountListVMs.length; i++) {
            if (response.distanceAmountListVMs.length == 1) {
                
                console.log(response.distanceAmountListVMs[0]);
                vehicleType = `<div class="col-lg-12 backroundAndBorderTema" data-v-identifier-value="${response.distanceAmountListVMs[0].vehicleTypeId}">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="imagesAllVehicles">
                                                        <p class="carType">${response.distanceAmountListVMs[0].typeName}</p>
                                                        <p class="vehiclePrice">$ ${response.distanceAmountListVMs[0].distanceAmount}</p>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="humanSuitcase">
                                                        <img src="https://img.icons8.com/ios/50/ADADAD/conference-call--v1.png" /><span>${response.distanceAmountListVMs[i].passengersCount}</span>
                                                        <img src="https://img.icons8.com/50/ADADAD/pastel-glyph/64/null/suitcase--v4.png"
                                                     style="padding-bottom: 8px;" /><span class="suitcases-count">${response.distanceAmountListVMs[i].suitCasesCount}</span>
                                                        <button  type="button"
                                                        class="btn btn-outline-secondary toggle-button-vehicle ${response.distanceAmountListVMs[0].isActive ? 'toggle-button-selected' : ''} ${response.distanceAmountListVMs[0].isActive ? 'aria-pressed=true' : ''}"  onclick="SelectedVehicleType(this)" data-type="${response.distanceAmountListVMs[0].typeName.replace(' ', '').toLowerCase()}"  data-type-total="${response.distanceAmountListVMs[0].typeName.replace(' ', '').toLowerCase()}-result">
                                                            Select
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>`

                $(".allvehicletypes").append(vehicleType);

            }
            else {
                vehicleType = `<div class="col-lg-12 backroundAndBorderTema" data-v-identifier-value="${response.distanceAmountListVMs[i].vehicleTypeId}">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="imagesAllVehicles">
                                                        <p class="carType">${response.distanceAmountListVMs[i].typeName}</p>
                                                        <p class="vehiclePrice">$ ${response.distanceAmountListVMs[i].distanceAmount}</p>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="humanSuitcase">
                                                        <img src="https://img.icons8.com/ios/50/ADADAD/conference-call--v1.png" /><span>${response.distanceAmountListVMs[i].passengersCount}</span>
                                                        <img src="https://img.icons8.com/50/ADADAD/pastel-glyph/64/null/suitcase--v4.png"
                                                     style="padding-bottom: 8px;" /><span class="suitcases-count">${response.distanceAmountListVMs[i].suitCasesCount}</span>
                                                        <button  type="button"
                                                        class="btn btn-outline-secondary toggle-button-vehicle ${response.distanceAmountListVMs[i].isActive ? 'toggle-button-selected' : ''} ${response.distanceAmountListVMs[i].isActive ? 'aria-pressed=true' : ''}"  onclick="SelectedVehicleType(this)" data-type="${response.distanceAmountListVMs[i].typeName.replace(' ', '').toLowerCase()}"  data-type-total="${response.distanceAmountListVMs[i].typeName.replace(' ', '').toLowerCase()}-result">
                                                            Select
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>`

                $(".allvehicletypes").append(vehicleType);
            }
        }

        if (response.id == 0) {

        }
        else {
            
        }

    });
}

