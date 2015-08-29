define(['accounting'], function (accounting) {
    Number.prototype.toPesos = function(){
        return accounting.formatMoney(this, { precision: 0 });
    }

    Number.prototype.toKms = function () {
        //return accounting.formatMoney(this, { precision: 0 });
        return accounting.formatNumber(this, 0, ".")
    }
});