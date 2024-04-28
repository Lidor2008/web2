
      let div = document.getElementsByTagName("div")[0];
      let timeLeftDiv = document.getElementsByClassName("timeLeft")[0];
      let button1 = document.getElementById("1s");
      let button3 = document.getElementById("3s");
      let button5 = document.getElementById("5s");
      let button10 = document.getElementById("10s");
      let seconds = 5;
      let intervalId;

      function handleclick() {
        div.innerText++;
      }

      function updateTime() {
        seconds -= 1;
        console.log(seconds);
        timeLeftDiv.innerText = seconds;

        if (seconds == 0) {
          clearInterval(intervalId);
        }
      }

      intervalId = setInterval(updateTime, 1000);


