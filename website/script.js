let div = document.getElementsByTagName("div")[0];
let timeLeftDiv = document.getElementsByClassName("timeLeft")[0];
let button1 = document.getElementById("1s");
let button3 = document.getElementById("3s");
let button5 = document.getElementById("5s");
let button10 = document.getElementById("10s");
let countDisplay = document.getElementById('clickCount');
let seconds = 0;
let intervalId;
let clickCount = 0;

let gameStarted = false;

function countClick() {
  clickCount++;
  countDisplay.textContent = clickCount;

  if (!gameStarted) {
    intervalId = setInterval(updateTime, 1000);
    gameStarted = true;
  }
}

function updateTime() {
  seconds -= 1;
  console.log(seconds);
  timeLeftDiv.innerText = seconds;

  if (seconds == 0) {
    clearInterval(intervalId);
  }
}



function Time(timeInSecs){
  seconds = timeInSecs;
  timeLeftDiv.innerText = seconds;
}
