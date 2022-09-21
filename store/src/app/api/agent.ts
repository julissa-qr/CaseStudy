import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import Swal from "sweetalert2";
import { store } from "../store/configureStore";


axios.defaults.baseURL = 'http://localhost:5000/api/';
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use(config => {
    const token = store.getState().account.user?.token;
    config.headers = {
        Authorization: `Bearer ${token}`,
    }
    return config;
}, (error) => {
    return Promise.reject(error);
});

// Request interceptor for API calls
axios.interceptors.request.use(async config => {
    const value = await store.getState().account.user?.token;
    //const keys = JSON.parse(value)
    config.headers = {
        // 'Authorization': `Bearer ${user?.token}`,
        'Accept': 'application/json',
    }
    return config;
})

axios.interceptors.response.use(response => {
    return response
}, (error: AxiosError) => {
    //console.log('caught by interceptor');

    const Toast = Swal.mixin({
        toast: true,
        position: 'bottom-right',
        iconColor: 'red',
        background: '#f27474',
        customClass: {
            popup: 'colored-toast'
        },

        showConfirmButton: false,
        timer: 1500,
        timerProgressBar: true
    })

    const { data, status } = error.response!;
    switch (status) {
        case 400:
            if (data.errors) {
                const modelStateErrors: string[] = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modelStateErrors.push(data.errors[key])
                    }
                }
                throw modelStateErrors.flat();
            }
            //toast.error(data.title);
            Toast.fire({
                icon: 'error',
                title: '400 -Bad request'
            })
            break;
        case 401:
            //toast.error((data.title) || ('401 - Unauthorised'));
            Toast.fire({
                icon: 'error',
                title: '401 - Unauthorized'
            })
            break;
        case 500:
            //toast.error(data.title);
            Toast.fire({
                icon: 'error',
                title: '500 - Internal server error'
            })
            break;
        default:
            break;
    }
    return Promise.reject(error.response);
})

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody),

}

const Catalog = {
    list: () => requests.get('products'),
    details: (id: number) => requests.get(`products/${id}`),
    fetchFilters: () => requests.get('products/filters')
}

const TestErrors = {
    get400Error: () => requests.get('buggy/bad-request'),
    get401Error: () => requests.get('buggy/unauthorised'),
    get404Error: () => requests.get('buggy/not-found'),
    get500Error: () => requests.get('buggy/server-error'),
    getValidationError: () => requests.get('buggy/validation-error'),
}

const Basket = {
    get: () => requests.get('basket'),
    addItem: (productId: number, quantity = 1) => requests.post(`basket?productId=${productId}&quantity=${quantity}`, {}),
    removeItem: (productId: number, quantity = 1) => requests.delete(`basket?productId=${productId}&quantity=${quantity}`),
}

const Account = {
    login: (values: any) => requests.post('account/login', values),
    register: (values: any) => requests.post('account/register', values),
    currentUser: () => requests.get('account/currentUser'),
    fetchAddress: () => requests.get('account/savedAddress')
}

const Orders = {
    list: () => requests.get('order'),
    fetch: (id: number) => requests.get(`order/${id}`),
    create: (values: any) => requests.post('order', values)
}

const agent = {
    Catalog,
    TestErrors,
    Basket,
    Account,
    Orders
}

export default agent;