define(['accounting'], function (accounting) {
    Number.prototype.toPesos = function(){
        return accounting.formatMoney(this, { precision: 0 });
    }
});