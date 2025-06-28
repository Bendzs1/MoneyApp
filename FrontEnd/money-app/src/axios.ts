import axios from "axios";

const instance = axios.create({
   baseURL: "http://localhost:5084/api", // <-- your backend URL here
   withCredentials: true, // if you're using cookies
});

export default instance;
