

var hor = true;

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
                    $('.error').css("display", "none")
                }
            }
            // END

            //Choose a Vehicle page validation section Start
            if (this.currentstep == 2) {
                if ($('#passengersSelect').val() === '') {


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

                }
                else {
                    $('#passengersError').css("display", "none")

                        this.$emit("step-change", this.currentstep + 1);
                  
                    
                }
            }
            //END

            //Enter Contact Details page validation section Start
            if (this.currentstep == 3) {
                if ($('#firstName').val() === '' || $('#lastName').val() === '' || $('#emailAddress').val() === '' || $('#phoneNumber').val() === '') {

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

                }
                else {
                    $('#firstNameError').css("display", "none")
                    $('#lastNameError').css("display", "none")
                    $('#emailAddressError').css("display", "none")
                    $('#phoneNumberError').css("display", "none")

                   this.$emit("step-change", this.currentstep + 1);
                    
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
})