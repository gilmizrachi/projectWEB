//initalises the facebook app annd connects it to the Facebook api for javascript
window.fbAsyncInit = function () {
    FB.init({
        appId: '4362830047125473',
        cookie: true,
        xfbml: true,        
        version: 'v8.0'
    });

    FB.api(
        "/102526684953651/feed",
        "POST",
        {
            "access_token":"EAA9ZCZBIcsnZBEBAKgZAPlYZBYRHJXOdk13HCdzZBjBfN0iB9ZCv7VTu5axqJUzrdr0HEb3VGUrYBfZBuMryZBnccnhXMrXOgW3hKmbmMiZBNIaeoD1pKtvlIlCoMPE3x5ztfNgDWKnL4REVyBQV0ZBlUpoVzaXnhqgKqaNocMA0YREBrYBQ53vaXx7ZC5QA8CEDZCXUZD",
            "message": "This is a test message"
        },
        function (response) {
            if (response && !response.error) {
                /* handle the result */
            }
        }
    );
};
