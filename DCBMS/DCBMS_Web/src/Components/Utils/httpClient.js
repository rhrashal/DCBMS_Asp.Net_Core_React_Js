import axios from 'axios';

const httpSimpleRequest = async (request) => {
 return await  axios({
        method: request.method,
        url: request.url,
        data: request.data,
        headers: request.headers
    })
    .then(response => {
        return response;
    }).catch(error => {
        return error;

    })
};

export {httpSimpleRequest}

