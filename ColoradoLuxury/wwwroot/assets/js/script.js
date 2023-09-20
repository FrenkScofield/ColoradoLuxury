

var hor = true;
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
                    this.$emit("step-change", this.currentstep + 1);
                    CalculateAmount(hor);
                    SaveRideDetailsInfo(this.currentstep);
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

                    } else {
                        $('#passengersGood').css("display", "none")
                        $('#passengersError').css("display", "block")
                    }

                } else if ($('#passengersSelect').val() >= 5) {

                    type.disabled = true;

                }
                else {
                    $('#passengersError').css("display", "none")

                    this.$emit("step-change", this.currentstep + 1);
                    SaveRideDetailsInfo(this.currentstep);

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

                                    this.$emit("step-change", this.currentstep + 1);
                                    SaveRideDetailsInfo(this.currentstep);
                                }

                            }
                        } else {
                            this.$emit("step-change", this.currentstep + 1);
                            SaveRideDetailsInfo(this.currentstep);
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

})
$('#distance').on('click', function () {
    hor = true;
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
            if (response.hasOwnProperty('distanceAmount') && response.hasOwnProperty('gratuity')) {

                $(".distanceAmount span").text(response.distanceAmount);
                $(".gratuity span").text(response.gratuity);
                $(".totalAmount span").text(response.totalAmount);
            } else {

            }
        });
    }
    else {
        let durationValue = $("#durationInHoursSelected").val();
        if (durationValue == '' || durationValue == undefined) {
            return;
        }
        AjaxPost("/Home/CalculatedAmount/", { hourly: hourly, durationValue: durationValue }, true, true, 'json', 'application/x-www-form-urlencoded; charset=UTF-8', (response) => {
            console.log(response);
            if (response.hasOwnProperty('distanceAmount') && response.hasOwnProperty('gratuity')) {

                $(".distanceAmount span").text(response.distanceAmount);
                $(".gratuity span").text(response.gratuity);
                $(".totalAmount span").text(response.totalAmount);
            } else {

            }
        });

    }
}

function SaveRideDetailsInfo(step) {
    let data = null;
    switch (step) {
        case 1:
            let pickupDate = $("#pickupDate").val();
            let time = $("#time").val();
            let pickuplocation = $("#pickuplocation").val();
            let dropOffLocation = $("#dropOffLocation").val();
            let transferTypeId = $("#transferType").val();

            if (transferTypeId == '')
                transferTypeId = 0;

            data = {
                WayType: hor,
                PickupDate: pickupDate,
                PickupTime: time,
                PickupLocation: pickuplocation,
                DropOffLocation: dropOffLocation,
                TransferTypeId: transferTypeId
            }
            console.log(data);
            AjaxPost("/RideDetails/AddDetails/", JSON.stringify(data), true, true, 'json', 'application/json; charset=utf-8', (response) => {
                console.log(response);
            });
            break;

        case 2:
            let passengersSelect = $("#passengersSelect").val();
            let suitcases = $("#suitcases").val();
            let allcarTpes = $("#allcarTpes").val();
            let childNumber = $("#childNumber").val();
            let roofCargoBoxNumber = $("#roofCargoBoxNumber").val();
            let childAdditionalMessage = $("#childAdditionalMessage").val();
            let roofCargoBoxAdditionalMessage = $("#roofCargoBoxAdditionalMessage").val();

            if (childNumber == '')
                childNumber = 0;

            if (roofCargoBoxNumber == '')
                roofCargoBoxNumber = 0;

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
            });
            break;

        case 3:
            let firstName = $("#firstName").val();
            let lastName = $("#lastName").val();
            let email = $("#emailAddress").val();
            let phoneNumber = $("#phoneNumber").val();
            let AdditionalContactDetailsNote = $("#additional-contact-details-note").val();
            let companyRegisteredName = $("#company-registered-name").val();
            let taxNumber = $("#taxNumber").val();

            let street = $("#street").val();
            let streetNumber = $("#streetNumber").val();
            let city = $("#city").val();
            let state = $("#state").val();
            let postalCode = $("#postalCode").val();
            let country = $("#country").val();
            let airline = $("#airline").val();
            let filingNumber = $("#filingNumber").val();


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
                FlightNumber: filingNumber
            }
            console.log(data);
            AjaxPost("/RideDetails/AddContactDetailsInfo/", JSON.stringify(data), true, true, 'json', 'application/json; charset=utf-8', (response) => {
                console.log(response);
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
