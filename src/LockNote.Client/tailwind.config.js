/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      animation: {
        float: "float 20s infinite ease-in-out",
      },
      keyframes: {
        float: {
          "0%": { transform: "translate(0, 0)" },
          "25%": { transform: "translate(50px, -30px)" },
          "50%": { transform: "translate(-50px, 30px)" },
          "75%": { transform: "translate(30px, -50px)" },
          "100%": { transform: "translate(0, 0)" },
        },
      },
    },
  },
  plugins: [],
};
