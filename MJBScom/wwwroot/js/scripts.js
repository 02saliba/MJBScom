function printSentence(sentence) {
  var charIndex = 0;
  var id = setInterval(printChar, 50);

  function printChar() {
    if (charIndex >= sentence.length) {
        clearInterval(id);
    } else {
        console.log(sentence[charIndex]);
        charIndex++;
    }
  }
}

function typewriter(sentences) {
  var charSpeed = 50;
  var sentenceSpeed = 1000;
  var time = 0;

  for (var i = 0; i < sentences.length; i++)
  {
    console.log(time);
    setTimeout(printSentence, time, sentences[i]);
    var time = sentences[i].length * charSpeed + sentenceSpeed;
  }
}



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

function httpGetAsync(theUrl)
{
    
}



$(document).ready(function(){

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

  $(window).on('keydown', function (e) {
    if (e.keyCode >= 37 && e.keyCode <= 40) {
      var xmlHttp = new XMLHttpRequest();
      xmlHttp.onreadystatechange = function() { 
          if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
              location.reload();
      }
      xmlHttp.open("GET", "/court/Model[0].GetId()/" + e.keyCode, true); // true for asynchronous 
      xmlHttp.send(null);
    }
  });
});
