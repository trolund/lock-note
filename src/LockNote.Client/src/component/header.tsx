import { Link } from "react-router-dom";

export const Header = () => {
  return (
    <div className="fixed w-screen h-14 top-0 left-0  bg-slate-950">
      <div className="flex align-middle gap-2">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="50"
          height="50"
          viewBox="0 0 200 200"
        >
          <rect
            x="50"
            y="90"
            width="100"
            height="80"
            rx="10"
            ry="10"
            fill="#2a9df4"
          />

          <path
            d="M70 90 V60 A30 30 0 0 1 130 60 V90"
            fill="none"
            stroke="#2a9df4"
            stroke-width="8"
          />

          <rect
            x="85"
            y="110"
            width="50"
            height="40"
            rx="5"
            ry="5"
            fill="#fff"
            stroke="#333"
            stroke-width="2"
          />
          <line
            x1="90"
            y1="120"
            x2="130"
            y2="120"
            stroke="#333"
            stroke-width="2"
          />
          <line
            x1="90"
            y1="130"
            x2="130"
            y2="130"
            stroke="#333"
            stroke-width="2"
          />
          <line
            x1="90"
            y1="140"
            x2="120"
            y2="140"
            stroke="#333"
            stroke-width="2"
          />
        </svg>

        <h1>
          <Link className="text-white text-xl align-middle" to="/">
            Lock-note
          </Link>
        </h1>
      </div>
    </div>
  );
};
