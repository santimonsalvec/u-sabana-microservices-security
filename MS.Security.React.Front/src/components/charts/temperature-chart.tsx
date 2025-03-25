import * as React from "react"
import { Bar, BarChart, CartesianGrid, XAxis } from "recharts"
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
    ChartTooltip,
    ChartTooltipContent,
} from "@/components/ui/chart"

import { testData } from "./temperature-test-data";

type WeatherRawData = {
    hourly: {
        time: string[];
        temperature_2m: number[];
    };
};

type WeatherFormatted = {
    date: string;
    temperature: number;
};

function formatToDateTimeString(isoString: string): string {
    const date = new Date(isoString);

    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); // meses 0-11
    const day = date.getDate().toString().padStart(2, '0');

    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');

    return `${year}-${month}-${day} ${hours}:${minutes}`;
}

function transformHourlyWeather(data: WeatherRawData): WeatherFormatted[] {
    const { time, temperature_2m } = data.hourly;
    const result: WeatherFormatted[] = [];

    for (let i = 0; i < time.length; i++) {
        result.push({
            date: time[i],
            temperature: temperature_2m[i],
        });
    }

    return result;
}

const chartData = transformHourlyWeather(testData);

const chartConfig = {
    views: {
        label: "Temp. en °C",
    },
    temperature: {
        label: "°C",
        color: "hsl(var(--chart-2))",
    },
} satisfies ChartConfig

const TemperatureChar = () => {
    const [activeChart, setActiveChart] =
        React.useState<keyof typeof chartConfig>("temperature")
    const refTemperature = React.useMemo(
        () => ({
            max: chartData.reduce((max, current) => current.temperature > max.temperature ? current : max).temperature,
            min: chartData.reduce((min, current) => current.temperature < min.temperature ? current : min).temperature
        }),
        []
    )
    return (
        <Card>
            <CardHeader className="flex flex-col items-stretch space-y-0 border-b p-0 sm:flex-row">
                <div className="flex flex-1 flex-col justify-center gap-1 px-6 py-5 sm:py-6">
                    <CardTitle>Pronóstico de temperatura</CardTitle>
                    <CardDescription>
                        Mostrando el pronóstico de temperatura para las siguientes horas
                    </CardDescription>
                </div>
                <div className="flex">
                    <button
                        key={"min-temperature"}
                        data-active={true}
                        className="relative z-30 flex flex-1 flex-col justify-center gap-1 border-t px-6 py-4 text-left even:border-l data-[active=true]:bg-muted/50 sm:border-l sm:border-t-0 sm:px-8 sm:py-6"
                        onClick={() => setActiveChart("temperature")}
                    >
                        <span className="text-xs text-muted-foreground">
                            Min °C
                        </span>
                        <span className="text-lg font-bold leading-none sm:text-3xl">
                            {[refTemperature.min].toString()}
                        </span>
                    </button>
                    <button
                        key={"max-temperature"}
                        data-active={true}
                        className="relative z-30 flex flex-1 flex-col justify-center gap-1 border-t px-6 py-4 text-left even:border-l data-[active=true]:bg-muted/50 sm:border-l sm:border-t-0 sm:px-8 sm:py-6"
                        onClick={() => setActiveChart("temperature")}
                    >
                        <span className="text-xs text-muted-foreground">
                            Max °C
                        </span>
                        <span className="text-lg font-bold leading-none sm:text-3xl">
                            {[refTemperature.max].toString()}
                        </span>
                    </button>
                </div>
            </CardHeader>
            <CardContent className="px-2 sm:p-6">
                <ChartContainer
                    config={chartConfig}
                    className="aspect-auto h-[150px] w-full"
                >
                    <BarChart
                        accessibilityLayer
                        data={chartData}
                        margin={{
                            left: 12,
                            right: 12,
                        }}
                    >
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
                            content={
                                <ChartTooltipContent
                                    className="w-[150px]"
                                    nameKey="views"
                                    labelFormatter={(value) => {
                                        return formatToDateTimeString(value)
                                    }}
                                />
                            }
                        />
                        <Bar dataKey={activeChart} fill={`var(--color-${activeChart})`} />
                    </BarChart>
                </ChartContainer>
            </CardContent>
        </Card>
    )
}

export default TemperatureChar;