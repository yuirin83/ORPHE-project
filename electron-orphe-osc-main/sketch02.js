var input = {
    x: [],
    y: []
}

var inputFixed = {
    x: [],
    y: []
};

var data = {
    triangle: {
        x: [-134, -138, -148, -163, -197, -213, -222, -233, -238, -239, -239, -239, -239, -239, -239, -239, -239, -239, -239, -239, -237, -234, -223, -210, -198, -172, -145, -112, -91, -84, -80, -78, -76, -75, -74, -74, -71, -69, -69, -69, -69, -69, -69, -68, -68, -68, -68, -70, -72, -77, -80, -86, -92, -97, -106, -113, -117, -117, -117, -117, -117, -118, -118, -119],
        y: [-177, -176, -170, -159, -145, -114, -98, -90, -77, -68, -65, -63, -62, -62, -62, -62, -62, -62, -62, -62, -62, -62, -62, -61, -58, -56, -52, -50, -49, -49, -49, -49, -48, -48, -48, -48, -48, -48, -48, -48, -48, -48, -49, -49, -50, -52, -53, -57, -62, -68, -86, -92, -109, -122, -135, -153, -168, -178, -181, -184, -184, -184, -184, -185, -186]
    },
    rectangle: {
        x: [-208, -208, -208, -207, -205, -203, -191, -176, -161, -153, -143, -130, -110, -97, -87, -78, -77, -77, -77, -77, -77, -76, -76, -76, -76, -76, -76, -76, -76, -76, -76, -76, -76, -76, -76, -76, -76, -75, -74, -71, -67, -65, -65, -65, -65, -65, -65, -65, -65, -64, -64, -64, -64, -64, -64, -63, -62, -62, -62, -62, -62, -62, -62, -62, -62, -62, -62, -64, -73, -83, -101, -112, -127, -140, -148, -154, -163, -169, -177, -180, -182, -186, -189, -190, -190, -190, -191, -192, -193, -193, -193, -193, -193, -193, -193, -193, -193, -193, -193, -193],
        y: [-27, -27, -27, -28, -28, -28, -27, -22, -17, -14, -14, -14, -14, -16, -19, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -24, -25, -28, -31, -39, -60, -84, -93, -123, -141, -154, -164, -168, -172, -181, -185, -186, -186, -186, -187, -190, -192, -193, -193, -194, -195, -195, -195, -195, -195, -195, -195, -195, -195, -194, -192, -192, -191, -192, -193, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194, -194]
    },
    circle: {
        x: [-169, -179, -186, -198, -205, -206, -207, -207, -206, -194, -176, -169, -146, -125, -113, -99, -92, -87, -82, -79, -76, -72, -71, -70, -69, -69, -69, -69, -69, -69, -68, -69, -73, -76, -80, -84, -89, -94, -96, -106, -116, -123, -127, -132, -133, -135, -135, -135, -136, -138, -139, -141, -142, -142, -142, -142, -142, -143, -143, -143, -143, -143, -143, -143, -143, -143, -143, -143, -143, -143, -143, -143, -143, -143, -143, -143, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144, -144],
        y: [-168, -159, -154, -142, -128, -117, -107, -93, -82, -68, -55, -52, -47, -45, -45, -45, -45, -46, -48, -50, -54, -63, -74, -83, -98, -111, -121, -131, -145, -152, -159, -164, -167, -169, -171, -174, -177, -179, -179, -182, -184, -186, -188, -189, -189, -189, -189, -189, -189, -189, -189, -189, -189, -189, -189, -189, -189, -189, -188, -188, -188, -188, -188, -188, -188, -188, -188, -188, -188, -188, -188, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187, -187]
    }
}


var dataLength = 50 * 2;

function setup() {
    createCanvas(256, 256);
    setInterval(function () {
        input.y.push(mouseY - 250);
        if (input.y.length > dataLength) {
            input.y.shift();
        }
    }, 20);
    setInterval(function () {
        input.x.push(mouseX - 250);
        if (input.x.length > dataLength) {
            input.x.shift();
        }
    }, 20);
}

function draw() {
    
    background(200);
    noFill();
    stroke(255, 0, 0);
    beginShape();
    let x = 0;
    for (let i of input.y) {
        vertex(x, i + 512 / 2);
        x++;
    }
    endShape();

    stroke(0, 255, 0);
    beginShape();
    let xx = 0;
    for (let i of input.x) {
        vertex(xx, i + 512 / 2);
        xx++;
    }
    endShape();

    if (input.x.length >= dataLength && input.y.length >= dataLength) {
        var distFunc = function (a, b) {
            return Math.abs(a - b);
        };


        // var dtw = {
        //     triangle: {
        //         x: new DynamicTimeWarping(input.x, data.triangle.x, distFunc),
        //         y: new DynamicTimeWarping(input.y, data.triangle.y, distFunc)
        //     },
        //     rectangle: {
        //         x: new DynamicTimeWarping(input.x, data.rectangle.x, distFunc),
        //         y: new DynamicTimeWarping(input.y, data.rectangle.y, distFunc)
        //     },
        //     circle: {
        //         x: new DynamicTimeWarping(input.x, data.circle.x, distFunc),
        //         y: new DynamicTimeWarping(input.y, data.circle.y, distFunc)
        //     }
        // }


        // let distance = [
        //     {
        //         id: 'triangle',
        //         value: (dtw.triangle.x.getDistance() + dtw.triangle.y.getDistance()) / 2
        //     },
        //     {
        //         id: 'rectangle',
        //         value: (dtw.rectangle.x.getDistance() + dtw.rectangle.y.getDistance()) / 2
        //     },
        //     {
        //         id: 'circle',
        //         value: (dtw.circle.x.getDistance() + dtw.circle.y.getDistance()) / 2
        //     },
        // ]

        var fixVal = {
            x: input.x[dataLength - 1] - -119,//入力点を基準の位置に補正する
            y: input.y[dataLength - 1] - -186
        }

        for(let i=0; i < input.x.length; i++){
            inputFixed.x[i] = input.x[i] - fixVal.x;
            inputFixed.y[i] = input.y[i] - fixVal.y;
        }

        var dtwFixed = {
            triangle: {
                x: new DynamicTimeWarping(inputFixed.x, data.triangle.x, distFunc),
                y: new DynamicTimeWarping(inputFixed.y, data.triangle.y, distFunc)
            },
            rectangle: {
                x: new DynamicTimeWarping(inputFixed.x, data.rectangle.x, distFunc),
                y: new DynamicTimeWarping(inputFixed.y, data.rectangle.y, distFunc)
            },
            circle: {
                x: new DynamicTimeWarping(inputFixed.x, data.circle.x, distFunc),
                y: new DynamicTimeWarping(inputFixed.y, data.circle.y, distFunc)
            }
        }

        let distanceFixed = [
            {
                id: 'triangle',
                value: (dtwFixed.triangle.x.getDistance() + dtwFixed.triangle.y.getDistance()) / 2
            },
            {
                id: 'rectangle',
                value: (dtwFixed.rectangle.x.getDistance() + dtwFixed.rectangle.y.getDistance()) / 2
            },
            {
                id: 'circle',
                value: (dtwFixed.circle.x.getDistance() + dtwFixed.circle.y.getDistance()) / 2
            },
        ]
        
        
        let result = distanceFixed.sort(function (a, b) {
            return (a.value < b.value) ? -1 : 1;
        });
        if (result[0].value < 1000) {
            textSize(36);
            fill(0);
            textAlign(CENTER, CENTER);
            text(result[0].id, width / 2, height / 2);
        }
        // console.log(result);
    }


}

function keyPressed() {
    if (key == ' ') {
        if (isLooping()) noLoop();
        else loop();
    }
    else if (key == 'o') {
        console.log(`[${inputFixed.x}];`);
        console.log(`[${inputFixed.y}];`);
    }
}