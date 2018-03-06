function countFrom20(agility, intelligence, strength, luck) {
  var valid = true;
  var count = 20;
  var agility = parseInt(agility);
  var intelligence = parseInt(intelligence);
  var strength = parseInt(strength);
  var luck = parseInt(luck);
  count = count - (agility + intelligence + strength + luck);
  if (count < 0) {
    alert("You are out of points!");
    valid = false;
  }
  else {
    valid = true;
  }
  console.log(valid);
  return valid;
}

$(document).ready(function(){

  console.log("Javascript loaded");


  $(".stat").change(function() {
    var agility = $("#agility").val();
    var intel = $("#intel").val();
    var strength = $("#strength").val();
    var luck = $("#luck").val();
    countFrom20(agility, intel, strength, luck);
  })

  $("#test-form").submit(function(event)
  {
    var agility = $("#agility").val();
    var intel = $("#intel").val();
    var strength = $("#strength").val();
    var luck = $("#luck").val();
    if(!countFrom20(agility, intel, strength, luck)) {

      event.preventDefault();
      alert("Character cannot be created with entered stats");
    }

  });

});
