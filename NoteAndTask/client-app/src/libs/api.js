export const request = async (url, method = "get", data) => {
    return fetch(url, {
        method,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer' + localStorage.getItem("token")
        },
        body: data
    }).then(response => {
        if(response.status === 200) {
            return response.json()
        }
        if(response.status === 401) {
            localStorage.removeItem("token");
            return window.location.href = "/signin"
        }
    })
};

export const logOut = () => {
    localStorage.removeItem("token");
    window.location.href = "/";
}