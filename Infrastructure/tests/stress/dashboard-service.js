import http from 'k6/http';
import { check } from 'k6';

export const options = {
    stages: [
        { duration: '30s', target: 10 },
        { duration: '2m', target: 10 },

        { duration: '30s', target: 20 },
        { duration: '2m', target: 20 },

        { duration: '1m', target: 0 }
    ],
    thresholds: {
        http_req_failed: ['rate<0.01'],
        http_req_duration: ['p(95)<15000'],
    },
};

export default function () {
    const res = http.get('http://127.0.0.1:52031/api/report/export');

    check(res, {
        'status is 200': (r) => r.status === 200,
    });
}



