import requests 
import sys

obj = {
    "search": sys.argv[1]
}

res = requests.post("http://94.237.60.169:58437/search.php", data="username=admin&password=admin", headers={"Agent": "curl/8.0.8"})
cookies = {
    "PHPSESSID": "78kbhktg7pd2di8fql7hgnv7ng"
}



response = requests.post("http://94.237.60.169:58437/search.php", json=obj, cookies=cookies, headers={"User-Agent": "curl/8.0.8", "Accept": "*/*", "Accept-Language": "en-US,en;q=0.5", "Content-Type": "application/json", "Referer": "http://94.237.60.169:58437/index.php"})
print(response.status_code) 
print(response.headers)
print(response.content.decode())   