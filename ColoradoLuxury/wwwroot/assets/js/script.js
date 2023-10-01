

var hor = true;
$('#distance').attr("selected", true)
var bill;
var airlineInfo;

Vue.component("step-navigation-step", {
    template: "#step-navigation-step-template",

    props: ["step", "currentstep"],

    computed: {
        indicatorclass() {
            return {
                active: this.step.id == this.currentstep,
                complete: this.currentstep > this.step.id
            };

        }
    }
});
Vue.component("step-navigation", {
    template: "#step-navigation-template",

    props: ["steps", "currentstep"]
});

Vue.component("step", {
    template: "#step-template",

    props: ["step", "stepcount", "currentstep"],

    computed: {
        active() {
            return this.step.id == this.currentstep;
        },

        firststep() {
            return this.currentstep == 1;
        },

        laststep() {
            return this.currentstep == this.stepcount;
        },

        stepWrapperClass() {
            return {
                active: this.active
            };

        }
    },

    methods: {
        nextStep() {
            var type = document.getElementById('carType');
            //Ride Details page validation section END
            let x = true;
            x = ($('#dropOffLocation').val() === '' && hor == false) ||
                ($('#pickuplocation').val() === '' && hor == false) ||
                ($('#dropOffLocation').val() != '' && hor == true) ? false : true;

            if ($('#pickupDate').val() === '' || $('#time').val() === '' || $('#pickuplocation').val() === '' || ($('#durationInHoursSelected').val() === '' && hor == false) || ($('#transferType').val() === '' && hor == true) || x) {

                $('#dateError').css("display", "block")
                $('#timeError').css("display", "block")
                $('#pickupError').css("display", "block")
                $('#dropError').css("display", "block")

                //Pickup date
                if ($('#pickupDate').val() != '') {
                    $('#dateGood').css("display", "block")
                    setTimeout(function () {
                        $('#dateGood').css("display", "none")
                    }, 1000);
                    $('#dateError').css("display", "none")
                } else {
                    $('#dateGood').css("display", "none")
                    $('#dateError').css("display", "block")
                }
                //Pickup time
                if ($('#time').val() != '') {
                    $('#timeGood').css("display", "block")
                    setTimeout(function () {
                        $('#timeGood').css("display", "none")
                    }, 1000);
                    $('#timeError').css("display", "none")
                }
                //Pickup location
                if ($('#pickuplocation').val() != '') {
                    $('#pickupGood').css("display", "block")
                    setTimeout(function () {
                        $('#pickupGood').css("display", "none")
                    }, 1000);
                    $('#pickupError').css("display", "none")
                } else {
                    $('#pickupGood').css("display", "none")
                    $('#pickupError').css("display", "block")
                }

                if (hor == true) {
                    //Drop-off location
                    if ($('#dropOffLocation').val() != '') {
                        $('#dropGood').css("display", "block")
                        setTimeout(function () {
                            $('#dropGood').css("display", "none")
                        }, 1000);
                        $('#dropError').css("display", "none")
                    } else {
                        $('#dropGood').css("display", "none")
                        $('#dropError').css("display", "block")
                    }

                    //Transfer Type
                    if ($('#transferType').val() != '') {
                        $('#transferTypeGood').css("display", "block")
                        setTimeout(function () {
                            $('#transferTypeGood').css("display", "none")
                        }, 1000);
                        $('#transferTypeError').css("display", "none")
                    } else {
                        $('#transferTypeGood').css("display", "none")
                        $('#transferTypeError').css("display", "block")
                    }

                } else {
                    //DURATION (IN HOURS)
                    if ($('#durationInHoursSelected').val() != '') {
                        $('#durationHoursGood').css("display", "block")
                        setTimeout(function () {
                            $('#durationHoursGood').css("display", "none")
                        }, 1000);
                        $('#durationHoursError').css("display", "none")
                    } else {
                        $('#durationHoursGood').css("display", "none")
                        $('#durationHoursError').css("display", "block")
                    }
                }
            }
            else {
                if (this.currentstep == 1) {
                    CalculateAmount(hor);
                    SaveRideDetailsInfo(this.currentstep);
                    this.$emit("step-change", this.currentstep + 1);
                    $('.error').css("display", "none")
                }
            }
            // END

            //Choose a Vehicle page validation section Start
            if (this.currentstep == 2) {
                if ($('#passengersSelect').val() === '') {
                    debugger
                    //Passenger
                    if ($('#passengersSelect').val() != '') {
                        $('#passengersGood').css("display", "block")
                        setTimeout(function () {
                            $('#passengersGood').css("display", "none")
                        }, 1000);
                        $('#passengersError').css("display", "none")

                    }
                    else {
                        $('#passengersGood').css("display", "none")
                        $('#passengersError').css("display", "block")
                    }

                }
                else if ($('#passengersSelect').val() >= 5) {

                    type.disabled = true;
                    SaveRideDetailsInfo(this.currentstep);
                    this.$emit("step-change", this.currentstep + 1);
                }
                else {
                    $('#passengersError').css("display", "none")

                    SaveRideDetailsInfo(this.currentstep);
                    this.$emit("step-change", this.currentstep + 1);

                }
            }
            //END

            //Enter Contact Details page validation section Start
            if (this.currentstep == 3) {

                bill = document.getElementById('billingAddress');
                airlineInfo = document.getElementById('airlineInfo')
                debugger

                if ($('#firstName').val() === '' || $('#lastName').val() === '' || $('#emailAddress').val() === '' || $('#phoneNumber').val() === '' || bill.checked == true || airlineInfo.checked == true) {

                    //FirstName
                    if ($('#firstName').val() != '') {
                        $('#firstNameGood').css("display", "block")
                        setTimeout(function () {
                            $('#firstNameGood').css("display", "none")
                        }, 1000);
                        $('#firstNameError').css("display", "none")
                    } else {
                        $('#firstNameGood').css("display", "none")
                        $('#firstNameError').css("display", "block")
                    }

                    //LastName
                    if ($('#lastName').val() != '') {
                        $('#lastNameGood').css("display", "block")
                        setTimeout(function () {
                            $('#lastNameGood').css("display", "none")
                        }, 1000);
                        $('#lastNameError').css("display", "none")
                    } else {
                        $('#lastNameGood').css("display", "none")
                        $('#lastNameError').css("display", "block")
                    }

                    //Email
                    if ($('#emailAddress').val() != '') {
                        $('#emailAddressGood').css("display", "block")
                        setTimeout(function () {
                            $('#emailAddressGood').css("display", "none")
                        }, 1000);
                        $('#emailAddressError').css("display", "none")
                    } else {
                        $('#emailAddressGood').css("display", "none")
                        $('#emailAddressError').css("display", "block")
                    }

                    //Phone
                    if ($('#phoneNumber').val() != '') {
                        $('#phoneNumberGood').css("display", "block")
                        setTimeout(function () {
                            $('#phoneNumberGood').css("display", "none")
                        }, 1000);
                        $('#phoneNumberError').css("display", "none")
                    } else {
                        $('#phoneNumberGood').css("display", "none")
                        $('#phoneNumberError').css("display", "block")
                    }

                    //BILLING ADDRESS Check section
                    if (bill.checked == true || airlineInfo.checked == true) {
                        if ($('#street').val() === '' || $('#city').val() === '' || ($('#state').val() === '' || $('#postalCode').val() === '' || $('#country').val() === '') || airlineInfo.checked == true) {
                            if ($('#street').val() != '') {
                                $('#streetGood').css("display", "block")
                                setTimeout(function () {
                                    $('#streetGood').css("display", "none")
                                }, 1000);
                                $('#streetError').css("display", "none")
                            } else {
                                $('#streetGood').css("display", "none")
                                $('#streetError').css("display", "block")
                            }

                            if ($('#city').val() != '') {
                                $('#cityGood').css("display", "block")
                                setTimeout(function () {
                                    $('#cityGood').css("display", "none")
                                }, 1000);
                                $('#cityError').css("display", "none")
                            } else {
                                $('#cityGood').css("display", "none")
                                $('#cityError').css("display", "block")
                            }

                            if ($('#state').val() != '') {
                                $('#stateGood').css("display", "block")
                                setTimeout(function () {
                                    $('#stateGood').css("display", "none")
                                }, 1000);
                                $('#stateError').css("display", "none")
                            } else {
                                $('#stateGood').css("display", "none")
                                $('#stateError').css("display", "block")
                            }

                            if ($('#postalCode').val() != '') {
                                $('#postalCodeGood').css("display", "block")
                                setTimeout(function () {
                                    $('#postalCodeGood').css("display", "none")
                                }, 1000);
                                $('#postalCodeError').css("display", "none")
                            } else {
                                $('#postalCodeGood').css("display", "none")
                                $('#postalCodeError').css("display", "block")
                            }

                            if ($('#country').val() != '') {
                                $('#countryGood').css("display", "block")
                                setTimeout(function () {
                                    $('#countryGood').css("display", "none")
                                }, 1000);
                                $('#countryError').css("display", "none")
                            } else {
                                $('#countryGood').css("display", "none")
                                $('#countryError').css("display", "block")
                            }


                            //ARRIVAL AIRLINE INFO Check section
                            if (airlineInfo.checked == true) {
                                if ($('#airline').val() === '' || $('#filingNumber').val() === '') {
                                    if ($('#airline').val() != '') {
                                        $('#airlineGood').css("display", "block")
                                        setTimeout(function () {
                                            $('#airlineGood').css("display", "none")
                                        }, 1000);
                                        $('#airlineError').css("display", "none")
                                    } else {
                                        $('#airlineGood').css("display", "none")
                                        $('#airlineError').css("display", "block")
                                    }

                                    if ($('#filingNumber').val() != '') {
                                        $('#filingNumberGood').css("display", "block")
                                        setTimeout(function () {
                                            $('#filingNumberGood').css("display", "none")
                                        }, 1000);
                                        $('#filingNumberError').css("display", "none")
                                    } else {
                                        $('#filingNumberGood').css("display", "none")
                                        $('#filingNumberError').css("display", "block")
                                    }
                                }
                                else {

                                    SaveRideDetailsInfo(this.currentstep);
                                    this.$emit("step-change", this.currentstep + 1);
                                }

                            }
                        } else {
                            SaveRideDetailsInfo(this.currentstep);
                            this.$emit("step-change", this.currentstep + 1);
                        }


                    }
                }
                else {
                    $('#firstNameError').css("display", "none")
                    $('#lastNameError').css("display", "none")
                    $('#emailAddressError').css("display", "none")
                    $('#phoneNumberError').css("display", "none")

                    this.$emit("step-change", this.currentstep + 1);
                    SaveRideDetailsInfo(this.currentstep);
                }
            }
            //END
        },

        lastStep() {
            this.$emit("step-change", this.currentstep - 1);
        }
    }
});

new Vue({
    el: "#app",

    data: {

        currentstep: 1,
        selected: '',

        steps: [
            {
                id: 1,
                title: "Enter Ride Details",
                icon_class: "fa fa-map-marker"
            },

            {
                id: 2,
                title: "Choose a Vehicle",
                icon_class: "fa fa-folder-open"
            },

            {
                id: 3,
                title: "Enter Contact Details",
                icon_class: "fa fa-paper-plane"
            },
            {
                id: 4,
                title: "Booking Summary",
                icon_class: "fa fa-paper-plane"
            }]
    },

    methods: {
        stepChanged(step) {
            this.currentstep = step;
        }
    }
});

const accordionItemHeaders = document.querySelectorAll(".accordion-item-header");

accordionItemHeaders.forEach(accordionItemHeader => {
    accordionItemHeader.addEventListener("click", event => {

        accordionItemHeader.classList.toggle("active");
        const accordionItemBody = accordionItemHeader.nextElementSibling;
        if (accordionItemHeader.classList.contains("active")) {
            accordionItemBody.style.maxHeight = 397 + "px";
        }
        else {
            accordionItemBody.style.maxHeight = 0;
        }

    });
});

//When this documend is ready, it will run
$('#hourly').on('click', function () {
    hor = false;
    $(this).attr("selected", true)
    $('#distance').attr("selected", true)
})
$('#distance').on('click', function () {
    hor = true;
    $(this).attr("selected", true);
    $('#hourly').attr("selected", true)

});

//when checkbox checked, then custom drive betting input enable will be
$('#customPriceActive').on('click', function () {

    $('#forDriveBett')[0].disabled = true;
    $('#btn0')[0].disabled = false;
    $('#btn10')[0].disabled = false;
    $('#btn20')[0].disabled = false;
    $('#btn30')[0].disabled = false;
    $('#btn40')[0].disabled = false;
    $('#btn50')[0].disabled = false;
    $('#bettButtons').css("opacity", "1");
    $('#customPrice').css("opacity", "0.1");
    $('#customBettAddBtn').css("display", "none")

    if ($('#customPriceActive').is(':checked') == true) {

        $('#forDriveBett')[0].disabled = false;
        $('#btn0')[0].disabled = true;
        $('#btn10')[0].disabled = true;
        $('#btn20')[0].disabled = true;
        $('#btn30')[0].disabled = true;
        $('#btn40')[0].disabled = true;
        $('#btn50')[0].disabled = true;
        $('#customPrice').css("opacity", "1");
        $('#bettButtons').css("opacity", "0.1");
        $('#customBettAddBtn').css("display", "block")

    }
})

//const button = document.querySelector('#customBettAddBtn');

//const disableButton = () => {
//   button.disabled = true;
//};
//button.addEventListener('click', disableButton);


$('#customBettAddBtn').on('click', function () {

    var CustomForDriveBetingPrice = $('#forDriveBett').val();

    if (CustomForDriveBetingPrice != "") {
        $('#customBettAddBtn').css("display", "none");
        $('#customBettSuccessfullAlert').css("display", "block");
        $('#forDriveBett')[0].disabled = true;

        setTimeout(AlertsucssesMessage, 3000);

        var asm = parseInt($('#forDriveBett').val());
        var sma = parseInt(document.getElementById('totalPrice').innerText);
        var sum = asm + sma
        document.getElementById('totalPrice').innerText = sum;

        $('#customPriceActive').css("display", "none")

    } else {
        $('#customBettErrorAlert').css("display", "block")
        setTimeout(AlertErrorMessage, 3000);
    }
});

function AlertsucssesMessage() {
    $('#customBettSuccessfullAlert').css("display", "none");
}
function AlertErrorMessage() {
    $('#customBettErrorAlert').css("display", "none");
}

let bonusPrices = document.getElementsByClassName("bettingMonyBtn")

console.log(bonusPrices);

let bonusPriceArray = [];

for (var price of bonusPrices) {
    //When the bet button is clicked, the total price will be calculated
    price.addEventListener('click', function (event) {
        console.log(event);

        let addedBonusPrice = event.target.defaultValue;

        if (bonusPriceArray.length != 0) {
            var totalPrice = document.getElementById("totalPrice");

            let returnDefaultTotalPrice = parseInt(totalPrice.innerText) - parseInt(bonusPriceArray[0]);

            bonusPriceArray.pop(bonusPriceArray[0]);

            console.log("pop bonus array ");
            console.log(bonusPriceArray);

            Append(returnDefaultTotalPrice + parseInt(addedBonusPrice));
            bonusPriceArray.push(addedBonusPrice);
        }
        else {
            var totalPrice = document.getElementById("totalPrice");

            Append(parseInt(totalPrice.innerText) + parseInt(addedBonusPrice));
            bonusPriceArray.push(addedBonusPrice);

            console.log("push bonus array ");
            console.log(bonusPriceArray);

        }
        function Append(text) {
            totalPrice.innerText = text

        }
    }, false);
}


 function CalculateAmount(hourly) {
    console.log(hourly);
    if (hourly) {
        AjaxPost("/Home/CalculatedAmount/", { hourly: hourly }, true, true, 'json', 'application/x-www-form-urlencoded; charset=UTF-8', (response) => {
            console.log(response);

            CalculatedAmountResponse(response);
        });
    }
    else {
        let durationValue = $("#durationInHoursSelected").val();
        if (durationValue == '' || durationValue == undefined) {
            return;
        }
        AjaxPost("/Home/CalculatedAmount/", { hourly: hourly, durationValue: durationValue }, true, true, 'json', 'application/x-www-form-urlencoded; charset=UTF-8', (response) => {
            console.log(response);

            CalculatedAmountResponse(response);
        });

    }
}

function CalculatedAmountResponse(response) {
    if (response.hasOwnProperty('vehicleTypeNotFound')) {
        alert("Something went wrong!");
        location.href = "/";
        return;
    }

    if (response.hasOwnProperty('notFoundMileValue')) {
        alert("Mile is not given this context!");
        location.href = "/";
        return;
    }
    

    let dataTypes = $(".toggle-button-vehicle");
    if (response.hasOwnProperty('getVehicleDistanceAmounts') && response.hasOwnProperty('getVehiclesIsActive')) {

        let allvehicle = [];
        console.log(response.getVehiclesIsActive);
        if (response.getVehiclesIsActive === null) {
            alert("Map is not selected");
            location.href = "/";
            return;
        }

        dataTypes.each(function () {
            allvehicle.push($(this).data('type'));

            for (var i = 0; i < response.getVehicleDistanceAmounts.length; i++) {
                if ($(this).data('type') == response.getVehicleDistanceAmounts[i].key) {
                    console.log(`$${response.getVehicleDistanceAmounts[i].distanceAmount}`);
                    $(this).parent().parent().parent()[0].children[0].children[0].children[1].innerText = `$${response.getVehicleDistanceAmounts[i].distanceAmount}`;
                    break;
                }
            }
        });

        console.log(response.getVehiclesIsActive.distanceAmount)
        console.log(response.getVehiclesIsActive.graduity)
        console.log(response.getVehiclesIsActive.totalAmount)


        $(".distanceAmount span").text(response.getVehiclesIsActive.distanceAmount);
        $(".gratuity span").text(response.getVehiclesIsActive.graduity);
        $(".totalAmount span").text(response.getVehiclesIsActive.totalAmount);
    } else {
    }
}

 function SaveRideDetailsInfo(step) {
     let data = null;
     let airlineAutoCheck = false;

    switch (step) {
        case 1:
            let dropOffLocation = null;
            let pickupDate = $("#pickupDate").val();
            let time = $("#time").val();
            let pickuplocation = $("#pickuplocation").val();
            console.log(hor)
            if (hor)
                dropOffLocation = $("#dropOffLocation").val();
            else
                dropOffLocation = $("#forHourlydropOffLocation").val();


            console.log(dropOffLocation)

            
            let transferTypeId = $("#transferType").val();
            let durationInHours = $("#durationInHoursSelected").val();

            if (durationInHours == '') {
                durationInHours = null;
            }

            if (transferTypeId == '')
                transferTypeId = 0;

            data = {
                WayType: hor,
                PickupDate: pickupDate,
                PickupTime: time,
                PickupLocation: pickuplocation,
                DropOffLocation: dropOffLocation,
                TransferTypeId: transferTypeId,
                DurationInHours: durationInHours
            }
            console.log(data);
            AjaxPost("/RideDetails/AddDetails/", JSON.stringify(data), true, true, 'json', 'application/json; charset=utf-8', (response) => {
                console.log(response);
            });
            break;

        case 2:
            let selectedVehicleTypeElement = $(".btn.btn-outline-secondary.toggle-button-vehicle.toggle-button-selected");
            let selectedVehicleTypeValue = selectedVehicleTypeElement.parent().parent().parent().parent().data("v-identifier-value");

            let passengersSelect = $("#passengersSelect").val();
            let suitcases = $("#suitcases").val();
            let allcarTpes = selectedVehicleTypeValue;
            let childNumber = $("#childNumber").val();
            let roofCargoBoxNumber = $("#roofCargoBoxNumber").val();
            let childAdditionalMessage = $("#childAdditionalMessage").val();
            let roofCargoBoxAdditionalMessage = $("#roofCargoBoxAdditionalMessage").val();

            console.log(allcarTpes)
            if (childNumber == '')
                childNumber = 0;

            if (roofCargoBoxNumber == '')
                roofCargoBoxNumber = 0;

            if (allcarTpes == '')
                allcarTpes = 0;

            data = {
                PassengersSelect: passengersSelect,
                Suitcases: suitcases,
                AllcarTpes: allcarTpes,
                ChildNumber: childNumber,
                RoofCargoBoxNumber: roofCargoBoxNumber,
                ChildAdditionalMessage: childAdditionalMessage,
                RoofCargoBoxAdditionalMessage: roofCargoBoxAdditionalMessage
            }
            console.log(data);
            AjaxPost("/RideDetails/AddVehiclesInfo/", JSON.stringify(data), true, true, 'json', 'application/json; charset=utf-8', (response) => {
                console.log(response);
                airlineAutoCheck = response.airlineAutoCheck;
                sessionStorage.setItem("airlineAutoCheck", airlineAutoCheck);
                console.log(sessionStorage.getItem("airlineAutoCheck"))
                if (response.airlineAutoCheck) {
                    $("#airlineInfo").parent().addClass("active");
                    $("#airlineInfo").parent().siblings(".accordion-item-body.billBackgroundColor").attr("style", "max-height: 397px;");
                    $("#airlineInfo").attr("checked", true)
                    $("#airlineInfo").prop("checked", true)

                }
            });
            break;

        case 3:
            let firstName = $("#firstName").val();
            let lastName = $("#lastName").val();
            let email = $("#emailAddress").val();
            let phoneNumber = $("#phoneNumber").val();
            let AdditionalContactDetailsNote = $("#additional-contact-details-note").val();
            let companyRegisteredName = null;
            let taxNumber = null;
            let street = null;
            let streetNumber = null;
            let city = null;
            let state = null;
            let postalCode = null;
            let country = null;
            let airline = null;
            let filingNumber = null;



            if (bill.checked) {
                companyRegisteredName = $("#company-registered-name").val();
                taxNumber = $("#taxNumber").val();
                street = $("#street").val();
                streetNumber = $("#streetNumber").val();
                city = $("#city").val();
                state = $("#state").val();
                postalCode = $("#postalCode").val();
                country = $("#country").val();

            }
            if (airlineInfo.checked) {
                airline = $("#airline").val();
                filingNumber = $("#filingNumber").val();
            }

            data = {
                Firstname: firstName,
                Lastname: lastName,
                Email: email,
                PhoneNumber: phoneNumber,
                AdditionalContactDetailNote: AdditionalContactDetailsNote,
                CompanyRegisteredname: companyRegisteredName,
                TaxNumber: taxNumber,
                Street: street,
                StreetNumber: streetNumber,
                City: city,
                State: state,
                PostalCode: postalCode,
                CountryId: country,
                BillingAddressStatus: bill.checked,
                AirLineStatus: airlineInfo.checked,
                AirlineId: airline,
                FlightNumber: filingNumber,
                AirlineAutoCheck: Boolean(sessionStorage.getItem("airlineAutoCheck"))
            }
            console.log(data);
            AjaxPost("/RideDetails/AddContactDetailsInfo/", JSON.stringify(data), true, true, 'json', 'application/json; charset=utf-8', (response) => {
                console.log(response);

                if (response.hasOwnProperty("wrongStatus")) {
                    alert("If PickupLocation or DropOfLocation choosen 'Denver International Airport (DEN), Peña Boulevard, Denver, CO, USA' international airport , must be selected 'ARRIVAL AIRLINE INFO' tab!");
                    return;
                }

                ShowAllDatasFilledFromInput("firstname", response.contactDetails.firstname);
                ShowAllDatasFilledFromInput("lastname", response.contactDetails.lastname);
                ShowAllDatasFilledFromInput("email", response.contactDetails.email);
                ShowAllDatasFilledFromInput("phoneNumber", response.contactDetails.phoneNumber);
                ShowAllDatasFilledFromInput("companyName", response.contactDetails.companyRegisteredname);
                ShowAllDatasFilledFromInput("taxNumber", response.contactDetails.taxNumber);

                ShowAllDatasFilledFromInput("airLineType", response.getTextForIdVM.airLine);
                ShowAllDatasFilledFromInput("flightNumber", response.contactDetails.flightNumber);
                ShowAllDatasFilledFromInput("additionalContactDetailsNote", response.contactDetails.additionalContactDetailNote);
                ShowAllDatasFilledFromInput("service-type", response.getTextForIdVM.wayType);
                ShowAllDatasFilledFromInput("pickupLocation", response.rideDetails.pickupLocation);
                ShowAllDatasFilledFromInput("dropOffLocation", response.rideDetails.dropOffLocation);
                ShowAllDatasFilledFromInput("pickupDateAndTime", response.getTextForIdVM.pickupDateAndTime);
                ShowAllDatasFilledFromInput("total-distance", response.getTextForIdVM.totalDistance);
                ShowAllDatasFilledFromInput("total-time", response.getTextForIdVM.distanceTime);

                ShowAllDatasFilledFromInput("vehicle-type", response.getTextForIdVM.vehicleType);
                ShowAllDatasFilledFromInput("childSeatCount", response.vehicleDetails.childNumber);
            });
            break;

        case 4:

            break;
        default:
    }
}

function ShowAllDatasFilledFromInput(id, text) {
    $(`.${id}`).text(text);
}

function SelectedVehicleType(currentElement) {

    document.querySelectorAll('.btn.btn-outline-secondary.toggle-button-vehicle').forEach(element => {
        element.classList.remove('toggle-button-selected');
        element.setAttribute("aria-pressed", false);
    });
    currentElement.setAttribute("aria-pressed", true);
    console.log(currentElement.getAttribute("data-type"));


    AjaxPost("/RideDetails/GetAmountByVehicle/", { sessionName: currentElement.getAttribute("data-type") }, true, true, 'json', 'application/x-www-form-urlencoded; charset=UTF-8', (response) => {
        console.log(response);
        $(".distanceAmount span").text(response.amount.distanceAmount);
        $(".gratuity span").text(response.amount.graduity);
        $(".totalAmount span").text(response.amount.totalAmount);

    });
}