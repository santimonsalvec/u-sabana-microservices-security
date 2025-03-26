import * as React from "react"
import { Area, AreaChart, CartesianGrid, XAxis } from "recharts"
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import {
    type ChartConfig,
    ChartContainer,
    ChartLegend,
    ChartLegendContent,
    ChartTooltip,
    ChartTooltipContent,
} from "@/components/ui/chart"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { useEffect, useState } from "react"

const formatToDateString = (timestamp: number) => {
    const date = new Date(timestamp);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); // los meses empiezan en 0
    const day = date.getDate().toString().padStart(2, '0');

    return `${year}-${month}-${day}`;
}

interface IData {
    prices: number[][],
    market_caps: number[][],
    total_volumes: number[][],
}

const CurrencyChart = () => {
    const [data, setData] = useState({} as IData);

    const chartData = data.prices.map(([timestamp, price], index) => {
        const marketCap = data.market_caps[index]?.[1] ?? 0;
        const totalVolume = data.total_volumes[index]?.[1] ?? 0;
    
        return {
            date: formatToDateString(timestamp),
            marketCap: marketCap,
            totalVolume: totalVolume,
        };
    });
    
    const chartConfig = {
        price: {
            label: "Price: ",
            color: "hsl(var(--chart-1))",
        },
        marketCap: {
            label: "Capitalización",
            color: "hsl(var(--chart-2))",
        },
        totalVolume: {
            label: "Volumen",
            color: "hsl(var(--chart-2))",
        },
    } satisfies ChartConfig

    const apiGatewayUrl = import.meta.env.VITE_API_GATEWAY_URL

    useEffect(() => {
        fetch(`${apiGatewayUrl}/api/currency-market`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${sessionStorage.getItem("token")}`,
                'Content-Type': 'application/json'
            }
        })
            .then(res => res.json())
            .then(_data => {
                setData(_data);
            })
            .catch(err => {
                console.error('Error:', err);
            });
    }, [])

    const [timeRange, setTimeRange] = React.useState("90d")
    const filteredData = chartData.filter((item) => {
        const date = new Date(item.date)
        const referenceDate = new Date()
        let daysToSubtract = 90
        if (timeRange === "30d") {
            daysToSubtract = 30
        } else if (timeRange === "7d") {
            daysToSubtract = 7
        }
        const startDate = new Date(referenceDate)
        startDate.setDate(startDate.getDate() - daysToSubtract)
        return date >= startDate
    })
    return (
        <Card>
            <CardHeader className="flex items-center gap-2 space-y-0 border-b py-5 sm:flex-row">
                <div className="grid flex-1 gap-1 text-center sm:text-left">
                    <CardTitle>Mercado de moneda - Interactivo</CardTitle>
                    <CardDescription>
                        Mostrando la capitalización del mercado y el volumen de negociación
                    </CardDescription>
                </div>
                <Select value={timeRange} onValueChange={setTimeRange}>
                    <SelectTrigger
                        className="w-[160px] rounded-lg sm:ml-auto"
                        aria-label="Select a value"
                    >
                        <SelectValue placeholder="Últimos 3 meses" />
                    </SelectTrigger>
                    <SelectContent className="rounded-xl">
                        <SelectItem value="90d" className="rounded-lg">
                            Últimos 3 meses
                        </SelectItem>
                        <SelectItem value="30d" className="rounded-lg">
                            Últimos 30 días
                        </SelectItem>
                        <SelectItem value="7d" className="rounded-lg">
                            Últimos 7 días
                        </SelectItem>
                    </SelectContent>
                </Select>
            </CardHeader>
            <CardContent className="px-2 pt-4 sm:px-6 sm:pt-6">
                <ChartContainer
                    config={chartConfig}
                    className="aspect-auto h-[250px] w-full"
                >
                    <AreaChart data={filteredData}>
                        <defs>
                            <linearGradient id="fillMarketCap" x1="0" y1="0" x2="0" y2="1">
                                <stop
                                    offset="5%"
                                    stopColor="black"
                                    stopOpacity={0.8}
                                />
                                <stop
                                    offset="95%"
                                    stopColor="black"
                                    stopOpacity={0.1}
                                />
                            </linearGradient>
                            <linearGradient id="fillTotalVolume" x1="0" y1="0" x2="0" y2="1">
                                <stop
                                    offset="5%"
                                    stopColor="red"
                                    stopOpacity={0.8}
                                />
                                <stop
                                    offset="95%"
                                    stopColor="red"
                                    stopOpacity={0.1}
                                />
                            </linearGradient>
                        </defs>
                        <CartesianGrid vertical={false} />
                        <XAxis
                            dataKey="date"
                            tickLine={false}
                            axisLine={false}
                            tickMargin={8}
                            minTickGap={32}
                        tickFormatter={(value) => {
                            const date = new Date(value)
                            return date.toLocaleDateString("en-US", {
                                month: "short",
                                day: "numeric",
                            })
                        }}
                        />
                        <ChartTooltip
                            cursor={false}
                            content={
                                <ChartTooltipContent
                                    labelFormatter={(value) => {
                                        return new Date(value).toLocaleDateString("es-US")
                                    }}
                                    indicator="dot"
                                />
                            }
                        />
                        <Area
                            dataKey="marketCap"
                            type="natural"
                            fill="url(#fillMarketCap)"
                            stroke="black"
                            stackId="a"
                        />
                        <Area
                            dataKey="totalVolume"
                            type="natural"
                            fill="url(#fillTotalVolume)"
                            stroke="red"
                            stackId="a"
                        />
                        <ChartLegend content={<ChartLegendContent />} />
                    </AreaChart>
                </ChartContainer>
            </CardContent>
        </Card>
    )
}

export default CurrencyChart;