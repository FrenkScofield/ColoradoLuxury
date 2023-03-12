Vue.component("step-navigation-step", {
    template: "#step-navigation-step-template",
  
    props: ["step", "currentstep"],
  
    computed: {
      indicatorclass() {
        return {
          active: this.step.id == this.currentstep,
          complete: this.currentstep > this.step.id };
  
      } } });

  Vue.component("step-navigation", {
    template: "#step-navigation-template",
  
    props: ["steps", "currentstep"] });
  
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
          active: this.active };
  
      } },
  
    methods: {
      nextStep() {
        this.$emit("step-change", this.currentstep + 1);
      },
  
      lastStep() {
        this.$emit("step-change", this.currentstep - 1);
      } } });
  
  new Vue({
    el: "#app",
  
    data: {
      currentstep: 1,
  
      steps: [
      {
        id: 1,
        title: "Enter Ride Details",
        icon_class: "fa fa-map-marker" },
  
      {
        id: 2,
        title: "Choose a Vehicle",
        icon_class: "fa fa-folder-open" },
  
      {
        id: 3,
        title: "Enter Contact Details",
        icon_class: "fa fa-paper-plane" },
        {
            id: 4,
            title: "Booking Summary",
            icon_class: "fa fa-paper-plane" }] },
  
    methods: {
      stepChanged(step) {
        this.currentstep = step;
      } } });

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
    if(accordionItemHeader.classList.contains("active")) {
      accordionItemBody.style.maxHeight = 397 + "px";
    }
    else {
      accordionItemBody.style.maxHeight = 0;
    }
    
  });
});
