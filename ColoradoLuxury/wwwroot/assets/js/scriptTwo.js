$("#distance").click(function(){
    $("#DropLocation").css({"display": "block"});
    $("#TransferType").css({"display": "block"});
    $("#extraTime").css({ "display": "block" });
    $("#durationInHours").css({"display": "none"});
    $("#forHourlyDropLocation").css({ "display": "none" });
    $("#distance").css({"background-color": "#C1A926"});
    $("#distance").css({"color": "#fff"});
    $("#hourly").css({"background-color": "#4F4F4F"});
   $("#hourly").css({"color": "#C1A926"});
});

$("#hourly").click(function(){
   $("#DropLocation").css({"display": "none"});
   $("#TransferType").css({ "display": "none" });
   $("#extraTime").css({ "display": "none" });
   $("#forHourlyDropLocation").css({ "display": "block" });
   $("#durationInHours").css({ "display": "block" });
   $("#hourly").css({"background-color": "#C1A926"});
   $("#hourly").css({"color": "#fff"});
   $("#distance").css({"background-color": "#4F4F4F"});
   $("#distance").css({"color": "#C1A926"});
  });

  $(document).on('click', '.toggle-button-vehicle', function(){
   $(this).toggleClass('toggle-button-selected');
   if ($(this).hasClass('toggle-button-selected')) {
     $('#vehicleSelect').attr('aria-pressed', true);
     $("#vehicleSelect").css({"color": "#fff"});
   } else {
     $('#vehicleSelect').attr('aria-pressed', false);
     $("#vehicleSelect").css({"color": "rgb(173 173 173)"});
   }
 });

  $(document).on('click', '.toggle-button-child', function(){
   $(this).toggleClass('toggle-button-selected');
   if ($(this).hasClass('toggle-button-selected')) {
     $('#childSeat').attr('aria-pressed', true);
     $("#childSeat").css({"color": "#fff"});
     $("#extraMessageChildwillSit").css({"display": "block"});
   } else {
     $('#childSeat').attr('aria-pressed', false);
     $("#childSeat").css({"color": "rgb(173 173 173)"});
     $("#extraMessageChildwillSit").css({"display": "none"});
   }
 });

 $(document).on('click', '.toggle-button-cargo', function(){
   $(this).toggleClass('toggle-button-selected');
   if ($(this).hasClass('toggle-button-selected')) {
     $('#cargoBox').attr('aria-pressed', true);
     $("#cargoBox").css({"color": "#fff"});
     $("#extraMessageluggage").css({"display": "block"});
   } else {
     $('#cargoBox').attr('aria-pressed', false);
     $("#cargoBox").css({"color": "rgb(173 173 173)"});
     $("#extraMessageluggage").css({"display": "none"});

   }
 });


$(document).on("click", "#distance", function () {
    Clear();
})
$(document).on("click", "#hourly", function () {
    Clear();
})

function Clear() {
    $('#pickupDate').val('')
    $('#time').val('')
    $('#pickuplocation').val('')
    $('#dropOffLocation').val('')
}

