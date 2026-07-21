import http from 'k6/http';
import { check } from 'k6';

export const options = {
    stages: [
        { duration: '1m', target: 20 },
        { duration: '3m', target: 20 },
        { duration: '1m', target: 0 },
    ],
};

export default function () {
    const res = http.get('http://127.0.0.1:PORT/');
    check(res, { 'status is 200': (r) => r.status === 200 });
}
