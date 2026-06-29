import http from 'k6/http';
import { check } from 'k6';

export const options = {
    stages: [
        { duration: '1m', target: 200 },
        { duration: '5m', target: 200 },

        { duration: '1m', target: 800 },
        { duration: '5m', target: 800 },

        { duration: '1m', target: 1000 },
        { duration: '5m', target: 1000 },

        { duration: '5m', target: 0 }
    ],
    thresholds: {
        http_req_failed: ['rate<0.01'],
        http_req_duration: ['p(95)<5000'],
    },
};

export default function () {
    const res = http.get('http://localhost:7072/api/report/export');

    check(res, {
        'status is 200': (r) => r.status === 200,
    });
}