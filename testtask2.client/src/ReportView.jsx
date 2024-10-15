import React, { useState, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';


const DisplayAccounts = ({ data, number, isGroup, isReport, isClass }) => {
    var textStyle = { fontWeight: 'normal' };
    if (isGroup || isReport || isClass){
        textStyle = { fontWeight: 'bold'}
    }
    return(
    <tr>
        {isClass ? (<td style={textStyle}>ПО КЛАССУ</td>) : isReport ? (<td style={textStyle}>БАЛАНС</td>) :  (<td style={textStyle}>{number}</td>)}
        <td style={textStyle} align='center'>
            {data.activeInBalance}
        </td>
        <td style={textStyle} align='center'>
            {data.passiveInBalance}
        </td>
        <td style={textStyle} align='center'>
            {data.debit}
        </td>
        <td style={textStyle} align='center'>
            {data.credit}
        </td>
        <td style={textStyle} align='center'>
            {data.activeOutBalance}
        </td>
        <td style={textStyle} align='center'>
            {data.passiveOutBalance}
        </td>
    </tr>
    )
}
const DisplayClasses = ({ data }) => (
    <tbody>
        <tr>
            <td align='center' colSpan={7}>
                <b>Класс {data.number} {data.name}</b>
            </td>
        </tr>
        {data.accounts.map(a => (
            <DisplayAccounts key={a.number} data={a} number={a.number} isClass={a.isClassResult} isGroup={a.isGroupResult} />
        ))}
    </tbody>
)

const ReportView = () => {
    const { reportId } = useParams();
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const loadData = async () => {
        try {
            const response = await fetch(`/api/Report/reports/${reportId}`);
            if (response.ok) {
                const data = await response.json();
                setData(data);
            } else {
                console.error('Failed to fetch data');
            }
        } finally {
            setLoading(false);
        }
    }
    useEffect(() => {
        loadData();
    }, [reportId]);
    if (loading) return <p>Loading...</p>;
    return (
        <div>
            <p>{data.bankName}</p>
            <p>{data.reportDescription}</p>
            <p>{data.creationDate}</p>
            <p>{data.currency}</p>
            <table border='1px' width='1500px'>
                <thead>
                    <tr>
                        <th rowSpan={2}>Б/сч</th>
                        <th colSpan={2}>ВХОДЯЩЕЕ САЛЬДО</th>
                        <th colSpan={2}>ОБОРОТЫ</th>
                        <th colSpan={2}>ИСХОДЯЩЕЕ САЛЬДО</th>
                    </tr>
                    <tr>
                        <th>Актив</th>
                        <th>Пассив</th>
                        <th>Дебет</th>
                        <th>Кредит</th>
                        <th>Актив</th>
                        <th>Пассив</th>
                    </tr>
                </thead>

                {data.classes.map(d => (
                    <DisplayClasses key={d.name} data={d} />
                ))}
                <DisplayAccounts data={data.account} number={data.account.number} isReport={true} />

            </table>
        </div >

    );
};

export default ReportView;