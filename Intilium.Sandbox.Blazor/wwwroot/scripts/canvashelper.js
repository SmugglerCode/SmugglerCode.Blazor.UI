window.canvashelper = {

    context: null,
    lastpoint: null,
    canvas: null,

    init: function (canvasId) {
        this.canvas = document.getElementById(canvasId);
        if (!this.canvas) {
            return {
                message: "No canvas could be found with id: " + canvasId,
                messageType: 2, // 2 = error, see MessageType in c# (enum)
                isSuccess: false
            };
        }

        this.context = this.canvas.getContext("2d");
        this.lastPoint = null;

        return {
            message: "Canvas has been initialized successsfuly with id: " + canvasId,
            messageType: 0, // 0 = info, see MessageType in c# (enum)
            isSuccess: true
        };
    },

    setStrokeColor(color) {
        if (!this.context) {
            return;        
        }
        this.context.strokeStyle = color;
    },

    setLineWidth(lineWidth) {
        if (!this.context) {
            return;
        }
        console.log("Width: " + lineWidth);
        this.context.lineWidth = lineWidth;
    },

    closePath() {
        if (!this.context) {
            return;
        }
        console.log("Closing the path");
        this.context.stroke();
        this.lastPoint = null;
        console.log("last point after closing: ", this.lastpoint);
    },

    drawLineTo(x, y) {

        if (!this.context) {
            return;
        }

        console.log("Last point", this.lastPoint)

        if (this.lastPoint == null) {
            console.log("Move", x, y)
            this.context.beginPath();
            this.context.moveTo(x, y);
        } else {
            console.log("Line to", x, y); 
            this.context.lineTo(x, y);
            this.context.stroke();
        }

        this.lastPoint = { x, y };
    },

    clear() {
        if (!this.context) return;
        this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
        this.lastPoint = null;
    }
}