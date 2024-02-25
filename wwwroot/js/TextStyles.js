consoleText(['Jamie Agricola.', 'Software Engineer'], document.querySelector('.person-name'), ['black', 'rebeccapurple']);

function consoleText(words, target, colors) {
    if (colors === undefined) colors = ['#fff'];
    var letterCount = 1;
    var x = 1;
    var waiting = false;
    target.setAttribute('style', 'color:' + colors[0]);
    window.setInterval(function () {

        if (letterCount === 0 && waiting === false) {
            waiting = true;
            target.innerHTML = words[0].substring(0, letterCount);
            window.setTimeout(function () {
                var usedColor = colors.shift();
                colors.push(usedColor);
                var usedWord = words.shift();
                words.push(usedWord);
                x = 1;
                target.setAttribute('style', 'color:' + colors[0]);
                letterCount += x;
                waiting = false;
            }, 1000)
        } else if (letterCount === words[0].length + 1 && waiting === false) {
            waiting = true;
            window.setTimeout(function () {
                x = -1;
                letterCount += x;
                waiting = false;
            }, 1000)
        } else if (waiting === false) {
            target.innerHTML = words[0].substring(0, letterCount);
            letterCount += x;
        }
    }, 120);
}

