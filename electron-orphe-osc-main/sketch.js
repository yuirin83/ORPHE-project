// var osc = new OSC();

window.addEventListener('DOMContentLoaded', () => {
    // bles: defined in coreToolkit.js
    let ble = bles[0]; // bles[0], bles[1] are predefined by CoreToolkit.js
    ble.setup();
    ble.onConnect = function (uuid) {
        console.log('onConnect:', uuid);
        document.querySelector(`#status${ble.id}`).innerText = 'ONLINE';
        document.querySelector(`#status${ble.id}`).classList = 'bg-primary text-white'
    }
    ble.onDisconnect = function () {
        document.querySelector(`#status${ble.id}`).innerText = 'OFFLINE';
        document.querySelector(`#status${ble.id}`).classList = 'bg-secondary text-white'
    }
    ble.gotAcc = function (acc) {
        // document.querySelector(`#acc${ble.id}`).innerText = `${acc.x.toFixed(2)}`;
        // console.log("acc : " + acc.x)
        // let message = new OSC.Message('/orphe/acc', acc.x, acc.y, acc.z);
        let message = new OSC.Message('Num', oscNum)
        let messageB = new OSC.Message('Status', oscStatus)
        osc.send(message);
        osc.send(messageB);
    }
    // ble.gotQuat = function (quat) {
    //     document.querySelector(`#quat${ble.id}`).innerText = `${quat.w.toFixed(2)}`;
    // }
    // ble.gotStride = function (stride) {
    //     document.querySelector(`#stride${ble.id}`).innerText = `${stride.x.toFixed(2)}`;
    // }
    ble.gotGyro = function (gyro) {
        // document.querySelector(`#gyro${ble.id}`).innerText = `${gyro.z.toFixed(2)}`;
        document.querySelector('#gyro').innerHTML = gyro.z;
        console.log(1000)
        let message = new OSC.Message('Num', oscNum)
        let messageB = new OSC.Message('Status', oscStatus)
        osc.send(message);
        osc.send(messageB);
    }

    buildCoreToolkit(document.querySelector('#toolkit_placeholder'),
        `CORE 0${ble.id + 1}`,
        ble.id);

    document.getElementById('send').addEventListener('click', () => {
        let message = new OSC.Message('/orphe/test', "Hello ORPHE CORE!!");
        osc.send(message);
    });

    osc.open(); // connect by default to ws://localhost:8080
})
