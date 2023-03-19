var hor = false;

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
        horli() {
            debugger

            $('#hourly').on('click', function () {
                hor = false;
               
            })
            $('#distance').on('click', function () {
                hor = true;
            })
            return hor;
        },
        nextStep() {
          
            
            console.log($('#pickupDate').val());
            console.log($('#time').val());
            console.log($('#pickuplocation').val());
            console.log($('#dropOffLocation').val());



            //Ride Details page validation section  START
            if ($('#pickupDate').val() === '' || $('#time').val() === '' || $('#pickuplocation').val() === '' || $('#dropOffLocation').val() === '') {

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
                debugger

                if (this.horli() == false) { //distance
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
                } else {
                    if (this.currentstep == 1) {
                        this.$emit("step-change", this.currentstep + 1);
                        $('.error').css("display", "none")
                    }
                }


            }
            else {
                if (this.currentstep == 1) {
                    this.$emit("step-change", this.currentstep + 1);
                    $('.error').css("display", "none")
                }
            }
            //Ride Details page validation section END
            if (this.currentstep == 2) {
                this.$emit("step-change", this.currentstep + 1);
                alert("Doru 2")
            }
            if (this.currentstep == 3) {
                this.$emit("step-change", this.currentstep + 1);
                alert("Doru 3")
            }
            // this.$emit("step-change", this.currentstep + 1);
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

        // Uncomment in case you only want to allow for the display of only one collapsed item at a time!

        // const currentlyActiveAccordionItemHeader = document.querySelector(".accordion-item-header.active");
        // if(currentlyActiveAccordionItemHeader && currentlyActiveAccordionItemHeader!==accordionItemHeader) {
        //   currentlyActiveAccordionItemHeader.classList.toggle("active");
        //   currentlyActiveAccordionItemHeader.nextElementSibling.style.maxHeight = 0;
        // }

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


