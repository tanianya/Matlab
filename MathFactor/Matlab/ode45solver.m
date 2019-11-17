function [t,y] = ode45solver(dydt,a,b,y0)
dydt = str2func(dydt);
[t,y] = ode45(dydt,[a b],y0');
t
y
end

