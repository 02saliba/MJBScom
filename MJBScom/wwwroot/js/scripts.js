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



$(document).ready(function(){

  console.log("Javascript loaded");
  typewriter(["this is a sentence", "this is another sentence"]);


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
    if (e.keyCode === 37) //left
    {
      window.location.pathname = "/court/Model[0].GetId()/37"
    }
    else if (e.keyCode == 38) //up
    {
      window.location.pathname = "/court/Model[0].GetId()/38"
    }
    else if (e.keyCode == 39) //right
    {
      window.location.pathname = "/court/Model[0].GetId()/39";
    }
    else if (e.keyCode === 40) //down
    {
      window.location.pathname = "/court/Model[0].GetId()/40"
    }
  });

});


