// Setting the Canvas width and height to fill the web browser.




let canvas = document.getElementById('canvas');

canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

let c = canvas.getContext('2d');
let numStars = 1000;
let stars = []; //Empty array
let size = 1;
let fl = canvas.width;
let centerX = canvas.width / 2;
let centerY = canvas.height / 2;
let speed = 100;

var timestamp = null;
var lastMouseX = null;
var lastMouseY = null;

document.querySelector('html').style.background = 'rgb(113, 89, 193) !important';

canvas.addEventListener("mousemove", function (e) {
  if (timestamp === null) {
    timestamp = Date.now();
    lastMouseX = e.screenX;
    lastMouseY = e.screenY;
    return;
  }

  var now = Date.now();
  var dt = now - timestamp;
  var dx = e.screenX - lastMouseX;
  var dy = e.screenY - lastMouseY;
  var speedX = Math.round(dx / dt * 100);
  var speedY = Math.round(dy / dt * 100);


  speed = ((speedX + speedY / 2) / 10) > 5 ? ((speedX + speedY / 2) / 10) : 5;

  timestamp = now;
  lastMouseX = e.screenX;
  lastMouseY = e.screenY;
});
for (let i = 0; i < numStars; i++) {
  stars[i] = new Star();
}

function Star() {
  this.x = Math.random() * canvas.width;    //x axis location
  this.y = Math.random() * canvas.height;    //y axis
  this.z = Math.random() * canvas.width;    //depth of star

  this.move = function () {
    this.z = this.z - speed;
    if (this.z <= 0) {
      this.z = canvas.width;
    }
  };

  this.show = function () {
    let x, y, s; //x-axis, y-axis, size
    x = (this.x - centerX) * (fl / this.z);
    x = x + centerX;

    y = (this.y - centerY) * (fl / this.z);
    y = y + centerY;

    s = size * (fl / this.z);

    c.beginPath();
    c.fillStyle = 'rgb(113, 89, 193)';
    c.arc(x, y, s, 0, Math.PI * 2);
    c.fill();
  };
}

function draw() {
  c.fillStyle = 'black';
  c.fillRect(0, 0, canvas.width, canvas.height);
  for (let i = 0; i < numStars; i++) {
    stars[i].show();
    stars[i].move();
  }
}

function update() {
  draw();
  window.requestAnimationFrame(update);
}

update();

$(() => {
  $('.zooom').hover(function () {
    $('.zooom-description').addClass('hover');
  }, function () {
    $('.zooom-description').removeClass('hover');
  });

  $('.nav-link').hover(() => {
    speed = 60;
  }, () => {
    speed = 5;
  });
});

setInterval(() => {
  if (size === 1)
    size = 1.5;
  else
    size = 1;
}, 300);