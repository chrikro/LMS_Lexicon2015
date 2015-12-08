    function ChangeRoll(id) {
        var e = document.getElementById(id);
        if (e.style.display == 'block') {
            e.style.display = 'none';
            document.getElementById('GroupStudent').style.display = 'block';
        }
        else {
            e.style.display = 'block';
            document.getElementById('GroupStudent').style.display = 'none';
        }
    }