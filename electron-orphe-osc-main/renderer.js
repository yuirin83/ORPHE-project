let input = []
let input_x = []
var mygyro = []
let last_timestamp = 0
let flg_show = false

var r, g, b
var milli_sec

var osc = new OSC()
var oscNum = 0
var gesture_status = 0
var oscStatus = 0

var distFunc = function (a, b) {
  return Math.abs(a - b)
}

window.onload = function () {
  bles[0].setup()

  // bles[0].gotAcc = function (acc) {}
  bles[0].gotGyro = function (gyro) {
    mygyro.push(gyro)
    while (mygyro.length > 500) {
      mygyro.shift()
    }
    input.push(gyro.z)
    while (input.length > 50) {
      input.shift()
    }
    input_x.push(gyro.x)
    while (input_x.length > 50) {
      input_x.shift()
    }

    if (input.length == 50) {
      let dtw1 = new DynamicTimeWarping(input, gesture.default, distFunc)
      let dtw2 = new DynamicTimeWarping(input, gesture.trigger, distFunc)
      let dtw3 = new DynamicTimeWarping(input, gesture.lifted, distFunc)
      let dtw4 = new DynamicTimeWarping(input, gesture.stop, distFunc)
      let dtw5 = new DynamicTimeWarping(input, gesture.katonn, distFunc)
      let dtw6 = new DynamicTimeWarping(input, gesture.suitonn, distFunc)
      let dtw7 = new DynamicTimeWarping(input, gesture.mokutonn, distFunc)
      let dtw8 = new DynamicTimeWarping(input, gesture.kuchiyose, distFunc)
      // let dtw9 = new DynamicTimeWarping(input, gesture.guruguru, distFunc)
      // let dtw10 = new DynamicTimeWarping(input, gesture.nejineji, distFunc)
      let d1 = dtw1.getDistance()
      let d2 = dtw2.getDistance()
      let d3 = dtw3.getDistance()
      let d4 = dtw4.getDistance()
      let d5 = dtw5.getDistance()
      let d6 = dtw6.getDistance()
      let d7 = dtw7.getDistance()
      let d8 = dtw8.getDistance()
      // let d9 = dtw9.getDistance()
      // let d10 = dtw10.getDistance()
      let d_array = [
        { id: 'default', value: d1 },
        { id: 'trigger', value: d2 },
        { id: 'lifted', value: d3 },
        { id: 'stop', value: d4 },
        { id: 'katonn', value: d5 },
        { id: 'suitonn', value: d6 },
        { id: 'mokutonn', value: d7 },
        { id: 'kuchiyose', value: d8 },
        // { id: 'guruguru', value: d9 },
        // { id: 'nejineji', value: d10 },
      ]

      let result = d_array.sort(function (a, b) {
        return a.value < b.value ? -1 : 1
      })

      let x_dtw1 = new DynamicTimeWarping(input_x, gesture_x.default, distFunc)
      let x_dtw2 = new DynamicTimeWarping(input_x, gesture_x.trigger, distFunc)
      let x_dtw3 = new DynamicTimeWarping(input_x, gesture_x.lifted, distFunc)
      let x_dtw4 = new DynamicTimeWarping(input_x, gesture_x.stop, distFunc)
      let x_dtw5 = new DynamicTimeWarping(input_x, gesture.katonn, distFunc)
      let x_dtw6 = new DynamicTimeWarping(input_x, gesture.suitonn, distFunc)
      let x_dtw7 = new DynamicTimeWarping(input_x, gesture.mokutonn, distFunc)
      let x_dtw8 = new DynamicTimeWarping(input_x, gesture.kuchiyose, distFunc)
      // let x_dtw9 = new DynamicTimeWarping(input_x, gesture.guruguru, distFunc)
      // let x_dtw10 = new DynamicTimeWarping(input_x, gesture.nejineji, distFunc)
      let x_d1 = x_dtw1.getDistance()
      let x_d2 = x_dtw2.getDistance()
      let x_d3 = x_dtw3.getDistance()
      let x_d4 = x_dtw4.getDistance()
      let x_d5 = x_dtw5.getDistance()
      let x_d6 = x_dtw6.getDistance()
      let x_d7 = x_dtw7.getDistance()
      let x_d8 = x_dtw8.getDistance()
      // let x_d9 = x_dtw9.getDistance()
      // let x_d10 = x_dtw10.getDistance()
      let x_d_array = [
        { id: 'default', value: x_d1 },
        { id: 'trigger', value: x_d2 },
        { id: 'lifted', value: x_d3 },
        { id: 'stop', value: x_d4 },
        { id: 'katonn', value: x_d5 },
        { id: 'suitonn', value: x_d6 },
        { id: 'mokutonn', value: x_d7 },
        { id: 'kuchiyose', value: x_d8 },
        // { id: 'guruguru', value: x_d9 },
        // { id: 'nejineji', value: x_d10 },
      ]

      let result_x = x_d_array.sort(function (a, b) {
        return a.value < b.value ? -1 : 1
      })

      if(result[0].id == result_x[0].id){
        switch(result[0].id){
          case 'default':
            gesture_status = 0;
            break
          case 'trigger':
            gesture_status = 1;
            break
          case 'lifted':
            gesture_status = 2;
            break
          case 'stop':
            gesture_status = 3;
            break
          case 'katonn':
            gesture_status = 4;
            break
          case 'suitonn':
            gesture_status = 5;
            break
          case 'mokutonn':
            gesture_status = 6;
            break
          case 'kuchiyose':
            gesture_status = 7;
            break
          case 'guruguru':
            gesture_status = 8;
            break
          case 'nejineji':
            gesture_status = 9;
            break
        }
      }else{
        gesture_status = 0;
      }

      let minDist
      let minId
      if(result[0].value <= result_x[0].value){
        if(result[0].value > 2 && result[0].id != 'default' && result[0].id != 'trigger' && result[0].id != 'lifted'){
          minDist = result[0].value
          minId = result[0].id
        }else{
          minDist = 0
          minId = 'default'
        }
      }else{
        if(result_x[0].value > 2 && result_x[0].id != 'default' && result_x[0].id != 'trigger' && result_x[0].id != 'lifted'){
          minDist = result_x[0].value
          minId = result_x[0].id
        }else{
          minDist = 0
          minId = 'default'
        }
      }

      document.querySelector('#test').innerHTML = `${minId}, `
      document.querySelector('#test2').innerHTML = `${minDist}, `
      document.querySelector('#gyro').innerHTML = `${gyro.z.toFixed(2)}, `

      console.log(gesture_status)
      oscNum = gesture_status;
      oscStatus = minId;

    }
    if (flg_show) {
      document.querySelector('#input_data_z').innerHTML += `${gyro.z}, `
      document.querySelector('#input_data_x').innerHTML += `${gyro.x}, `
    }
  }

  // document
  //   .getElementById('game-title')
  //   .addEventListener('DOMContentLoaded', () => {
  //     let message = new OSC.Message('Num:', oscNum)
  //     osc.send(message)
  //   })
  osc.open()
}

const s = (p) => {
  p.setup = () => {
    console.log('Start!')
    r = 0
    g = 0
    b = 0
    milli_sec = 0

    let canvas = p.createCanvas(500, 300)
    canvas.parent('canvas')
    p.background(r, g, b)
  }

  p.draw = () => {
    milli_sec = p.millis()
    // console.log(milli_sec)
    p.background(r, g, b)
    p.noFill()

    let x = 0
    p.stroke(255, 0, 0)
    p.beginShape()
    for (let g of mygyro) {
      p.vertex(x, 100 * g.x + 150)
      x++
    }
    p.endShape()

    x = 0
    p.stroke(0, 0, 255)
    p.beginShape()
    for (let g of mygyro) {
      p.vertex(x, 100 * g.z + 150)
      x++
    }
    p.endShape()

    
  }
}

const app = new p5(s)

document.addEventListener('keypress', keypress_event)
function keypress_event(e) {
  if (e.key === 'r') {
    flg_show = !flg_show
  }
  if (e.key == 'd') {
    document.querySelector('#input_data_z').innerText = ''
    document.querySelector('#input_data_x').innerText = ''
  }
  return false
}

const video = document.getElementById("video")
navigator.mediaDevices.getUserMedia({
    video: true,
    audio: false,
}).then(stream => {
    video.srcObject = stream;
    video.play()
}).catch(e => {
  console.log(e)
})
