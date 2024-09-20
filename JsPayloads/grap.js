document.addEventListener("DOMContentLoaded", async () => {

    const flagElem = document.querySelector(".flag");

    var response = await fetch(
        "http://94.237.60.169:58437/search.php",
        {
            method: "POST",
            headers: {
                Cookie: "PHPSESSIONID=78kbhktg7pd2di8fql7hgnv7ng",
                Agent: "curl/8.8.0"
            },
            body: "{\"search\":\"flag\"}"
        }
    );

    console.log(response);

});